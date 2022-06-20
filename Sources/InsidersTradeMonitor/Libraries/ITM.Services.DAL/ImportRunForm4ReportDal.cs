

using ITM.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.Services.Dal
{
    [Export(typeof(IImportRunForm4ReportDal))]
    public class ImportRunForm4ReportDal : DalBaseImpl<ImportRunForm4Report, Interfaces.IImportRunForm4ReportDal>, IImportRunForm4ReportDal
    {

        public ImportRunForm4ReportDal(Interfaces.IImportRunForm4ReportDal dalImpl) : base(dalImpl)
        {
        }

        public ImportRunForm4Report Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<ImportRunForm4Report> GetByImportRunID(System.Int64 ImportRunID)
        {
            return _dalImpl.GetByImportRunID(ImportRunID);
        }
        public IList<ImportRunForm4Report> GetByForm4ReportID(System.Int64 Form4ReportID)
        {
            return _dalImpl.GetByForm4ReportID(Form4ReportID);
        }
            }
}
