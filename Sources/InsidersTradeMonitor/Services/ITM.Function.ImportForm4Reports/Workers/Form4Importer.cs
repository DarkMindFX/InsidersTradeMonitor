

using ITM.Function.ImportForm4Reports.Helpers;
using ITM.Source.SEC;
using System;
using System.Collections.Generic;
using System.IO;

namespace ITM.Function.ImportForm4Reports.Workers
{
    public class Form4ImporterParams
    {
        public string CIK { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public ITM.Interfaces.ISource Source { get; set; }

        public ITM.Interfaces.IFilingParser FilingParser { get; set; }

        public IForm4DalWrapper Form4DalWrappwer { get; set; }

        public IImportRunDalFacade ImportRunDalFacade { get; set; }

        public Interfaces.Entities.ImportRun ImportRun { get; set; }
    }

    public class Form4Importer
    {
        private Form4ImporterParams _importerParams = null;
        private string _processId = null;
        private bool _isRunning = false;
        private IList<long> _reportIds = null;


        public Form4Importer(Form4ImporterParams impParams)
        {
            _importerParams = impParams;
            _processId = Guid.NewGuid().ToString();
            _reportIds = new List<long>();
        }

        public string ProcessID
        {
            get
            {
                return _processId;
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
        }

        public IList<long> ImportedReportsIDs
        {
            get
            {
                return _reportIds;
            }
        }

        public IList<long> Import()
        {
            if (!_isRunning)
            {
                _reportIds = new List<long>();
                ImporterThread();
                return _reportIds;
            }
            else
            {
                return null;
            }
        }

        protected void ImporterThread()
        {
            _isRunning = true;

            var validateParams = _importerParams.Source.CreateValidateParams();
            validateParams.CIK = _importerParams.CIK;
            validateParams.UpdateFromDate = _importerParams.DateFrom;
            validateParams.UpdateToDate = _importerParams.DateTo;
            _reportIds.Clear();

            // getting list of filings for CIK
            var resultFuilings = _importerParams.Source.GetFilingsList(validateParams).Result;

            if (resultFuilings.Success)
            {
                var getInfoParams = _importerParams.Source.CreateSourceSubmissionsInfoParams();
                getInfoParams.CIK = _importerParams.CIK;
                resultFuilings.Filings.ForEach(
                    f => getInfoParams.Items.Add(new SECSourceItemInfo() { Name = f.Name })
                );

                // getting submissions info - list of files - for each filing
                var resultSubInfo = _importerParams.Source.GetSubmissionsInfo(getInfoParams).Result;

                if (resultSubInfo.Success)
                {

                    var extractParams = _importerParams.Source.CreateExtractParams();
                    resultSubInfo.Submissions.ForEach(s =>
                    {
                        if (s.Type == "4")
                        {
                            extractParams.Items.Add(new SECSourceItemInfo()
                            {
                                Name = s.Name
                            });
                        }
                    });
                    extractParams.CIK = _importerParams.CIK;

                    // getting filings content for Form 4 filings
                    var extractResults = _importerParams.Source.ExtractReports(extractParams).Result;
                    if (extractResults.Success)
                    {
                        // parsing reports
                        foreach (var i in extractResults.Items)
                        {
                            DateTime dtStart = DateTime.UtcNow; // start time

                            MemoryStream ms = new MemoryStream(i.Content.ToArray());
                            var parserParams = _importerParams.FilingParser.CreateFilingParserParams();
                            parserParams.FileContent = ms;
                            parserParams.ReportID = i.FilingName;

                            var resultParse = _importerParams.FilingParser.Parse(parserParams);
                            if (resultParse.Success)
                            {
                                // Save statement to storage 
                                long newReportID = _importerParams.Form4DalWrappwer.InsertReport(resultParse.Statement);
                                _reportIds.Add(newReportID);

                                // adding run/report info 
                                ITM.Interfaces.Entities.ImportRunForm4Report importRunForm4Report = new Interfaces.Entities.ImportRunForm4Report()
                                {
                                    Form4ReportID = newReportID,
                                    ImportRunID = (long)_importerParams.ImportRun.ID,
                                    TimeStarted = dtStart,
                                    TimeCompleted = DateTime.UtcNow
                                };
                                importRunForm4Report = _importerParams.ImportRunDalFacade.InsertImportRunForm4Report(importRunForm4Report);

                            }

                        }

                    }
                }
            }

            _isRunning = false;
        }

    }
}
