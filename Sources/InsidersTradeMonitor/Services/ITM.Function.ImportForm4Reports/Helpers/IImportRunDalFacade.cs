using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Function.ImportForm4Reports.Helpers
{
    public interface IImportRunDalFacade
    {
        ImportRun InsertImportRun(ImportRun importRun);

        ImportRun SetRunSucceeded(long runId);

        ImportRun SetRunFailed(long runId);

        ImportRun UpdateImportRun(ImportRun importRun);

        ImportRunForm4Report InsertImportRunForm4Report(ImportRunForm4Report importRunForm4Report);

        ImportRunForm4Report UpdateImportRunForm4Report(ImportRunForm4Report importRunForm4Report);
    }
}
