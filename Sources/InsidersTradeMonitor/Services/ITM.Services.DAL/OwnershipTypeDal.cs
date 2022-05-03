


using ITM.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.Services.Dal
{
    [Export(typeof(IOwnershipTypeDal))]
    public class OwnershipTypeDal : DalBaseImpl<OwnershipType, Interfaces.IOwnershipTypeDal>, IOwnershipTypeDal
    {

        public OwnershipTypeDal(Interfaces.IOwnershipTypeDal dalImpl) : base(dalImpl)
        {
        }

        public OwnershipType Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


            }
}
