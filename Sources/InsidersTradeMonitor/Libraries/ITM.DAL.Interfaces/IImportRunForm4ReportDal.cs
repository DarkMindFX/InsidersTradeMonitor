


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITM.Interfaces.Entities;
using ITM.Interfaces;

namespace ITM.Interfaces
{
    public interface IImportRunForm4ReportDal : IDalBase<ImportRunForm4Report>
    {
        ImportRunForm4Report Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<ImportRunForm4Report> GetByImportRunID(System.Int64 ImportRunID);
        IList<ImportRunForm4Report> GetByForm4ReportID(System.Int64 Form4ReportID);
        
            }
}

