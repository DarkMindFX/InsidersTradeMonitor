


using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITM.Services.Dal
{
    public interface IForm4ReportDal : IDalBase<Form4Report>
    {
        Form4Report Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Form4Report> GetByIssuerID(System.Int64 IssuerID);
            IList<Form4Report> GetByReporterID(System.Int64 ReporterID);
    
        }
}
