


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(IEntityTypeDal))]
    public class EntityTypeDal : DalBaseImpl<EntityType, Interfaces.IEntityTypeDal>, IEntityTypeDal
    {

        public EntityTypeDal(Interfaces.IEntityTypeDal dalImpl) : base(dalImpl)
        {
        }

        public EntityType Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


            }
}
