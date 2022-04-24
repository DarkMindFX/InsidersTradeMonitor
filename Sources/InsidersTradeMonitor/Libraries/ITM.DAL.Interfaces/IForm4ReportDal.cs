


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITM.Interfaces.Entities;
using ITM.Interfaces;

namespace ITM.Interfaces
{
    public interface IForm4ReportDal : IDalBase<Form4Report>
    {
        Form4Report Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<Form4Report> GetByIssuerID(System.Int64 IssuerID);
        IList<Form4Report> GetByReporterID(System.Int64 ReporterID);
        
            }
}

