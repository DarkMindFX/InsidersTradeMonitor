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

namespace ITM.Function.V1.ImportForm4Reports
{
    public class StartImportFn 
    {
        private readonly IForm4DalWrapper _form4DalWrapper;
        public StartImportFn(IForm4DalWrapper form4DalWrapper)
        {
            _form4DalWrapper = form4DalWrapper;
        }

        [FunctionName("StartImport")]
        public void Run([QueueTrigger("itm-notifications", Connection = "AzureWebJobsStorage")] string message)
        {
            MessageBase msgObject = JsonSerializer.Deserialize<MessageBase>(message);
            if(msgObject != null && msgObject.Name.Equals("StartImport"))
            {
                RpcStartImport request = JsonSerializer.Deserialize<RpcStartImport>(msgObject.Payload);
                if(request != null)
                {
                    ReportsIDs = Import(request, _form4DalWrapper);
                }
            }
        }

        public IList<long> ReportsIDs
        {
            get; set;
        }

        protected IList<long> Import(RpcStartImport request, IForm4DalWrapper form4DalWrapper)
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
                Source = source
            };

            var importer = new Form4Importer(impParams);
            return importer.Import();
        }
    }
}
