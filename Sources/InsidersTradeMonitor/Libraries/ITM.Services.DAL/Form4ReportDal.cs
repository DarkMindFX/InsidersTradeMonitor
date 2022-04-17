


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(IForm4ReportDal))]
    public class Form4ReportDal : DalBaseImpl<Form4Report, Interfaces.IForm4ReportDal>, IForm4ReportDal
    {

        public Form4ReportDal(Interfaces.IForm4ReportDal dalImpl) : base(dalImpl)
        {
        }

        public Form4Report Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<Form4Report> GetByIssuerID(System.Int64 IssuerID)
        {
            return _dalImpl.GetByIssuerID(IssuerID);
        }
        public IList<Form4Report> GetByReporterID(System.Int64 ReporterID)
        {
            return _dalImpl.GetByReporterID(ReporterID);
        }
            }
}
