using System;
using System.Collections.Generic;
using System.Text.Json;
using ITM.DTO;
using ITM.Function.ImportForm4Reports.Helpers;
using ITM.Function.ImportForm4Reports.Workers;
using ITM.Functions.Common;
using ITM.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ITM.Function.V1.ImportForm4Reports
{
    public class StartImportFn 
    {
        private readonly IForm4DalWrapper _form4DalWrapper;
        private readonly IImportRunDalFacade _importRunDalFacade;
        public StartImportFn(IForm4DalWrapper form4DalWrapper, IImportRunDalFacade importRunDalFacade)
        {
            _form4DalWrapper = form4DalWrapper;
            _importRunDalFacade = importRunDalFacade;
        }

        [FunctionName("StartImport")]
        [return: Queue("itm-reports-imported", Connection = "AzureWebJobsStorage")]
        public MessageBase Run([QueueTrigger("itm-import-requests", Connection = "AzureWebJobsStorage")] string message)
        {
            MessageBase msgRes = null;
            MessageBase msgObject = JsonSerializer.Deserialize<MessageBase>(message);
            if(msgObject != null)
            {
                if (msgObject.Name.Equals("StartImport"))
                {
                    ITM.Interfaces.Entities.ImportRun importRun = null;
                    try
                    {
                        importRun = LogRunStarted(message);

                        RpcStartImport request = JsonSerializer.Deserialize<RpcStartImport>(msgObject.Payload);
                        if (request != null)
                        {
                            ReportsIDs = Import(request, importRun, _form4DalWrapper, _importRunDalFacade);                            

                            importRun = LogRunSucceeded(importRun);

                            msgRes = PrepareReportsImportedResp(ReportsIDs, importRun);
                            
                        }
                        else
                        {
                            throw new ArgumentException("StartImport message: failed to cast Paylod to type RpcStartImport");
                        }
                    }
                    catch
                    {
                        importRun = LogRunFailed(importRun);
                        throw;
                    }
                }
            }
            else
            {
                throw new ArgumentException("Failed to parse incoming message - skipping");
            }

            return msgRes;
        }

        public IList<long> ReportsIDs
        {
            get; set;
        }

        protected IList<long> Import(RpcStartImport request, ITM.Interfaces.Entities.ImportRun importRun, IForm4DalWrapper form4DalWrapper, IImportRunDalFacade importRunDalFacade)
        {
            var parser = new ITM.Parser.Form4.Form4Parser();

            var source = new ITM.Source.SEC.SECSource();
            ISourceInitParams sourceInitParams = source.CreateInitParams();
            sourceInitParams.Logger = new ITM.Logging.NullLogger();

            var impParams = new Form4ImporterParams()
            {
                CIK = request.CIK,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                FilingParser = parser,
                Form4DalWrappwer = form4DalWrapper,
                ImportRunDalFacade = importRunDalFacade,
                ImportRun = importRun,
                Source = source
            };

            var importer = new Form4Importer(impParams);
            return importer.Import();
        }

        #region Support methods

        protected void SendOutputQueueMessage(CloudQueue queue, MessageBase msg)
        {
            var cqm = new CloudQueueMessage(JsonSerializer.Serialize(msg));
            queue.AddMessageAsync(cqm);

        }

        protected MessageBase PrepareReportsImportedResp(IList<long> reportIDs, ITM.Interfaces.Entities.ImportRun importRun)
        {
            MessageBase msgResponse = new MessageBase();

            DTO.ReportsImported dtoReportsImported = new ReportsImported();
            dtoReportsImported.ImportRunID = (long)importRun.ID;
            dtoReportsImported.ReportIDs = reportIDs;

            msgResponse.Name = "Form4ImportResults";
            msgResponse.Payload = JsonSerializer.Serialize(dtoReportsImported);

            return msgResponse;
        }

        protected ITM.Interfaces.Entities.ImportRun LogRunStarted(string message)
        {
            var importRun = new ITM.Interfaces.Entities.ImportRun()
            {
                RequestJson = message,
                StateID = (long)Interfaces.Entities.EImportRunState.InProgress,
                TimeStart = DateTime.UtcNow
            };

            importRun = _importRunDalFacade.InsertImportRun(importRun);
            return importRun;
        }

        protected ITM.Interfaces.Entities.ImportRun LogRunSucceeded(ITM.Interfaces.Entities.ImportRun importRun)
        {
            importRun = _importRunDalFacade.SetRunSucceeded((long)importRun.ID);
            return importRun;
        }

        protected ITM.Interfaces.Entities.ImportRun LogRunFailed(ITM.Interfaces.Entities.ImportRun importRun)
        {
            importRun = _importRunDalFacade.SetRunFailed((long)importRun.ID);
            return importRun;
        }

        #endregion
    }
}
