


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class EntityDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IEntityDal))]
    public class EntityDal : IEntityDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new EntityDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.Entities.Find(ID);
            if (entity != null)
            {
                dbContext.Remove(entity);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public ITM.Interfaces.Entities.Entity Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.Entity result = null;
            var entity = dbContext.Entities.Where(e => e.ID == ID).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.EntityConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.Entity> GetAll()
        {
            var entities = dbContext.Entities.ToList();

            IList<ITM.Interfaces.Entities.Entity> result = ToList(entities);

            return result;
        }

        public IList<ITM.Interfaces.Entities.Entity> GetByEntityTypeID(System.Int64 EntityTypeID)
        {
            var entities = dbContext.Entities.Where(e => e.EntityTypeID == EntityTypeID).ToList();

            IList<ITM.Interfaces.Entities.Entity> result = ToList(entities);

            return result;
        }

        public IList<Interfaces.Entities.Entity> GetMonitoredList()
        {
            var entities = dbContext.Entities.Where(e => e.IsMonitored).ToList();

            IList<ITM.Interfaces.Entities.Entity> result = ToList(entities);

            return result;
        }

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.Entity Insert(ITM.Interfaces.Entities.Entity entity)
        {
            ITM.Interfaces.Entities.Entity result = null;
            var efEntity = Convertors.EntityConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.Entity>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.EntityConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.Entity Update(ITM.Interfaces.Entities.Entity entity)
        {
            ITM.Interfaces.Entities.Entity result = null;
            var efEntity = dbContext.Entities.Where(e => e.ID == entity.ID).FirstOrDefault();
            if (efEntity != null)
            {
                efEntity.EntityTypeID = entity.EntityTypeID;
                efEntity.CIK = entity.CIK;
                efEntity.Name = entity.Name;
                efEntity.TradingSymbol = entity.TradingSymbol;
                efEntity.IsMonitored = entity.IsMonitored;
                dbContext.SaveChanges();

                efEntity = dbContext.Entities.Where(e => e.ID == entity.ID).FirstOrDefault();
                result = Convertors.EntityConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.Entity> ToList(IList<ITM.DAL.EF.Models.Entity> entities)
        {
            IList<ITM.Interfaces.Entities.Entity> result = new List<ITM.Interfaces.Entities.Entity>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.EntityConvertor.FromEFEntity(e));
                }
            }
            return result;
        }

        #endregion
    }
}