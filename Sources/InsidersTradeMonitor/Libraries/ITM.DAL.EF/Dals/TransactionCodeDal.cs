


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class TransactionCodeDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(ITransactionCodeDal))]
    public class TransactionCodeDal : ITransactionCodeDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new TransactionCodeDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.TransactionCodes.Find(ID);
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


        public ITM.Interfaces.Entities.TransactionCode Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.TransactionCode result = null;
            var entity = dbContext.TransactionCodes.Where(e =>         e.ID = ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.TransactionCodeConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.TransactionCode> GetAll()
        {
            var entities = dbContext.TransactionCodes.ToList();

            IList<ITM.Interfaces.Entities.TransactionCode> result = ToList(entities);
            
            return result;
        }

                

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.TransactionCode Insert(ITM.Interfaces.Entities.TransactionCode entity)
        {
            ITM.Interfaces.Entities.TransactionCode result = null;
            var efEntity = Convertors.TransactionCodeConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.TransactionCode>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.TransactionCodeConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.TransactionCode Update(ITM.Interfaces.Entities.TransactionCode entity)
        {
            ITM.Interfaces.Entities.TransactionCode result = null;
            var efEntity = dbContext.TransactionCodes.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.Code = entity.Code;
						efEntity.Description = entity.Description;
		                dbContext.SaveChanges();

                efEntity = dbContext.TransactionCodes.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.TransactionCodeConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.TransactionCode> ToList(IList<ITM.DAL.EF.Models.TransactionCode> entities)
        {
            IList<ITM.Interfaces.Entities.TransactionCode> result = new List<ITM.Interfaces.Entities.TransactionCode>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.TransactionCodeConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}