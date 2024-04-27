using ITM.Function.ImportForm4Reports.Helpers;
using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Test.Function.ImportForm4Reports
{
    internal class NullImportRunDalFacade : IImportRunDalFacade
    {
        public ImportRun InsertImportRun(ImportRun importRun)
        {
            return new ImportRun() { ID = Int64.MaxValue };
        }

        public ImportRunForm4Report InsertImportRunForm4Report(ImportRunForm4Report importRunForm4Report)
        {
            return new ImportRunForm4Report() { ID = Int64.MaxValue };
        }

        public ImportRun SetRunFailed(long runId)
        {
            return new ImportRun() { ID = runId };
        }

        public ImportRun SetRunSucceeded(long runId)
        {
            return new ImportRun() { ID = runId };
        }

        public ImportRun UpdateImportRun(ImportRun importRun)
        {
            return importRun;
        }

        public ImportRunForm4Report UpdateImportRunForm4Report(ImportRunForm4Report importRunForm4Report)
        {
            return importRunForm4Report;
        }
    }
}
