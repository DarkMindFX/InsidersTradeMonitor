


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class EntityTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IEntityTypeDal))]
    public class EntityTypeDal : IEntityTypeDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new EntityTypeDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.EntityTypes.Find(ID);
            if (entity != null)
            {
                entity.IsDeleted = true;
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public ITM.Interfaces.Entities.EntityType Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.EntityType result = null;
            var entity = dbContext.EntityTypes.Where(e =>         e.ID = ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.EntityTypeConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.EntityType> GetAll()
        {
            var entities = dbContext.EntityTypes.ToList();

            IList<ITM.Interfaces.Entities.EntityType> result = ToList(entities);
            
            return result;
        }

                

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.EntityType Insert(ITM.Interfaces.Entities.EntityType entity)
        {
            ITM.Interfaces.Entities.EntityType result = null;
            var efEntity = Convertors.EntityTypeConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.EntityType>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.EntityTypeConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.EntityType Update(ITM.Interfaces.Entities.EntityType entity)
        {
            ITM.Interfaces.Entities.EntityType result = null;
            var efEntity = dbContext.EntityTypes.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.TypeName = entity.TypeName;
		                dbContext.SaveChanges();

                efEntity = dbContext.EntityTypes.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.EntityTypeConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.EntityType> ToList(IList<ITM.DAL.EF.Models.EntityType> entities)
        {
            IList<ITM.Interfaces.Entities.EntityType> result = new List<ITM.Interfaces.Entities.EntityType>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.EntityTypeConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}