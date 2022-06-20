


using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITM.Services.Dal
{
    public interface IImportRunForm4ReportDal : IDalBase<ImportRunForm4Report>
    {
        ImportRunForm4Report Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<ImportRunForm4Report> GetByImportRunID(System.Int64 ImportRunID);
            IList<ImportRunForm4Report> GetByForm4ReportID(System.Int64 Form4ReportID);
    
        }
}
