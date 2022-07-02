


using ITM.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.Services.Dal
{
    [Export(typeof(IEntityDal))]
    public class EntityDal : DalBaseImpl<Entity, Interfaces.IEntityDal>, IEntityDal
    {

        public EntityDal(Interfaces.IEntityDal dalImpl) : base(dalImpl)
        {
        }

        public Entity Get(System.Int64 ID)
        {
            return _dalImpl.Get(ID);
        }

        public bool Delete(System.Int64 ID)
        {
            return _dalImpl.Delete(ID);
        }


        public IList<Entity> GetByEntityTypeID(System.Int64 EntityTypeID)
        {
            return _dalImpl.GetByEntityTypeID(EntityTypeID);
        }

        public IList<Entity> GetMonitoredList()
        {
            return _dalImpl.GetMonitoredList();
        }
    }
}
