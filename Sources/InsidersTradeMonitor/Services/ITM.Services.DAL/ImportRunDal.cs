

using ITM.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.Services.Dal
{
    [Export(typeof(IImportRunDal))]
    public class ImportRunDal : DalBaseImpl<ImportRun, Interfaces.IImportRunDal>, IImportRunDal
    {

        public ImportRunDal(Interfaces.IImportRunDal dalImpl) : base(dalImpl)
        {
        }

        public ImportRun Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<ImportRun> GetByStateID(System.Int64 StateID)
        {
            return _dalImpl.GetByStateID(StateID);
        }
            }
}
