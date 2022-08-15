


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class OwnershipTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IOwnershipTypeDal))]
    public class OwnershipTypeDal : IOwnershipTypeDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new OwnershipTypeDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.OwnershipTypes.Find(ID);
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


        public ITM.Interfaces.Entities.OwnershipType Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.OwnershipType result = null;
            var entity = dbContext.OwnershipTypes.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.OwnershipTypeConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.OwnershipType> GetAll()
        {
            var entities = dbContext.OwnershipTypes.ToList();

            IList<ITM.Interfaces.Entities.OwnershipType> result = ToList(entities);
            
            return result;
        }

                

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.OwnershipType Insert(ITM.Interfaces.Entities.OwnershipType entity)
        {
            ITM.Interfaces.Entities.OwnershipType result = null;
            var efEntity = Convertors.OwnershipTypeConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.OwnershipType>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.OwnershipTypeConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.OwnershipType Update(ITM.Interfaces.Entities.OwnershipType entity)
        {
            ITM.Interfaces.Entities.OwnershipType result = null;
            var efEntity = dbContext.OwnershipTypes.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.Code = entity.Code;
						efEntity.Description = entity.Description;
		                dbContext.SaveChanges();

                efEntity = dbContext.OwnershipTypes.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.OwnershipTypeConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.OwnershipType> ToList(IList<ITM.DAL.EF.Models.OwnershipType> entities)
        {
            IList<ITM.Interfaces.Entities.OwnershipType> result = new List<ITM.Interfaces.Entities.OwnershipType>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.OwnershipTypeConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}