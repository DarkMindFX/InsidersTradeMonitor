

using ITM.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.Services.Dal
{
    [Export(typeof(IImportRunStateDal))]
    public class ImportRunStateDal : DalBaseImpl<ImportRunState, Interfaces.IImportRunStateDal>, IImportRunStateDal
    {

        public ImportRunStateDal(Interfaces.IImportRunStateDal dalImpl) : base(dalImpl)
        {
        }

        public ImportRunState Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


            }
}
