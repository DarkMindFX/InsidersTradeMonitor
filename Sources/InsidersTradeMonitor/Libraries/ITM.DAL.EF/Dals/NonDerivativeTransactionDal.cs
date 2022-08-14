


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class NonDerivativeTransactionDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(INonDerivativeTransactionDal))]
    public class NonDerivativeTransactionDal : INonDerivativeTransactionDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new NonDerivativeTransactionDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.NonDerivativeTransactions.Find(ID);
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


        public ITM.Interfaces.Entities.NonDerivativeTransaction Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.NonDerivativeTransaction result = null;
            var entity = dbContext.NonDerivativeTransactions.Where(e => e.ID == ID).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.NonDerivativeTransactionConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.NonDerivativeTransaction> GetAll()
        {
            var entities = dbContext.NonDerivativeTransactions.ToList();

            IList<ITM.Interfaces.Entities.NonDerivativeTransaction> result = ToList(entities);

            return result;
        }

        public IList<NonDerivativeTransaction> GetByForm4ReportID(System.Int64 Form4ReportID)
        {
            var entities = dbContext.NonDerivativeTransactions.Where(e => e.Form4ReportID == Form4ReportID).ToList();

            IList<ITM.Interfaces.Entities.NonDerivativeTransaction> result = ToList(entities);

            return result;
        }
        public IList<NonDerivativeTransaction> GetByTransactionCodeID(System.Int64? TransactionCodeID)
        {
            var entities = dbContext.NonDerivativeTransactions.Where(e => e.TransactionCodeID == TransactionCodeID).ToList();

            IList<ITM.Interfaces.Entities.NonDerivativeTransaction> result = ToList(entities);

            return result;
        }
        public IList<NonDerivativeTransaction> GetByTransactionTypeID(System.Int64? TransactionTypeID)
        {
            var entities = dbContext.NonDerivativeTransactions.Where(e => e.TransactionTypeID == TransactionTypeID).ToList();

            IList<ITM.Interfaces.Entities.NonDerivativeTransaction> result = ToList(entities);

            return result;
        }
        public IList<NonDerivativeTransaction> GetByOwnershipTypeID(System.Int64? OwnershipTypeID)
        {
            var entities = dbContext.NonDerivativeTransactions.Where(e => e.OwnershipTypeID == OwnershipTypeID).ToList();

            IList<ITM.Interfaces.Entities.NonDerivativeTransaction> result = ToList(entities);

            return result;
        }


        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.NonDerivativeTransaction Insert(ITM.Interfaces.Entities.NonDerivativeTransaction entity)
        {
            ITM.Interfaces.Entities.NonDerivativeTransaction result = null;
            var efEntity = Convertors.NonDerivativeTransactionConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.NonDerivativeTransaction>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.NonDerivativeTransactionConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.NonDerivativeTransaction Update(ITM.Interfaces.Entities.NonDerivativeTransaction entity)
        {
            ITM.Interfaces.Entities.NonDerivativeTransaction result = null;
            var efEntity = dbContext.NonDerivativeTransactions.Where(e => e.ID == entity.ID).FirstOrDefault();
            if (efEntity != null)
            {
                efEntity.Form4ReportID = entity.Form4ReportID;
                efEntity.TitleOfSecurity = entity.TitleOfSecurity;
                efEntity.TransactionDate = entity.TransactionDate;
                efEntity.DeemedExecDate = entity.DeemedExecDate;
                efEntity.TransactionCodeID = entity.TransactionCodeID;
                efEntity.EarlyVoluntarilyReport = entity.EarlyVoluntarilyReport;
                efEntity.SharesAmount = entity.SharesAmount;
                efEntity.TransactionTypeID = entity.TransactionTypeID;
                efEntity.Price = entity.Price;
                efEntity.AmountFollowingReport = entity.AmountFollowingReport;
                efEntity.OwnershipTypeID = entity.OwnershipTypeID;
                efEntity.NatureOfIndirectOwnership = entity.NatureOfIndirectOwnership;
                dbContext.SaveChanges();

                efEntity = dbContext.NonDerivativeTransactions.Where(e => e.ID == entity.ID).FirstOrDefault();
                result = Convertors.NonDerivativeTransactionConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.NonDerivativeTransaction> ToList(IList<ITM.DAL.EF.Models.NonDerivativeTransaction> entities)
        {
            IList<ITM.Interfaces.Entities.NonDerivativeTransaction> result = new List<ITM.Interfaces.Entities.NonDerivativeTransaction>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.NonDerivativeTransactionConvertor.FromEFEntity(e));
                }
            }
            return result;
        }

        #endregion
    }
}