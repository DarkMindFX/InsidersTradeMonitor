


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class TransactionTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(ITransactionTypeDal))]
    public class TransactionTypeDal : ITransactionTypeDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new TransactionTypeDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.TransactionTypes.Find(ID);
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


        public ITM.Interfaces.Entities.TransactionType Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.TransactionType result = null;
            var entity = dbContext.TransactionTypes.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.TransactionTypeConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.TransactionType> GetAll()
        {
            var entities = dbContext.TransactionTypes.ToList();

            IList<ITM.Interfaces.Entities.TransactionType> result = ToList(entities);
            
            return result;
        }

                

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.TransactionType Insert(ITM.Interfaces.Entities.TransactionType entity)
        {
            ITM.Interfaces.Entities.TransactionType result = null;
            var efEntity = Convertors.TransactionTypeConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.TransactionType>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.TransactionTypeConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.TransactionType Update(ITM.Interfaces.Entities.TransactionType entity)
        {
            ITM.Interfaces.Entities.TransactionType result = null;
            var efEntity = dbContext.TransactionTypes.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.Code = entity.Code;
						efEntity.Description = entity.Description;
		                dbContext.SaveChanges();

                efEntity = dbContext.TransactionTypes.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.TransactionTypeConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.TransactionType> ToList(IList<ITM.DAL.EF.Models.TransactionType> entities)
        {
            IList<ITM.Interfaces.Entities.TransactionType> result = new List<ITM.Interfaces.Entities.TransactionType>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.TransactionTypeConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}