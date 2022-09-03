


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class DerivativeTransactionDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IDerivativeTransactionDal))]
    public class DerivativeTransactionDal : IDerivativeTransactionDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new DerivativeTransactionDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.DerivativeTransactions.Find(ID);
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


        public ITM.Interfaces.Entities.DerivativeTransaction Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.DerivativeTransaction result = null;
            var entity = dbContext.DerivativeTransactions.Where(e => e.ID == ID).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.DerivativeTransactionConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.DerivativeTransaction> GetAll()
        {
            var entities = dbContext.DerivativeTransactions.ToList();

            IList<ITM.Interfaces.Entities.DerivativeTransaction> result = ToList(entities);

            return result;
        }

        public IList<ITM.Interfaces.Entities.DerivativeTransaction> GetByForm4ReportID(System.Int64 Form4ReportID)
        {
            var entities = dbContext.DerivativeTransactions.Where(e => e.Form4ReportID == Form4ReportID).ToList();

            IList<ITM.Interfaces.Entities.DerivativeTransaction> result = ToList(entities);

            return result;
        }
        public IList<ITM.Interfaces.Entities.DerivativeTransaction> GetByTransactionCodeID(System.Int64 TransactionCodeID)
        {
            var entities = dbContext.DerivativeTransactions.Where(e => e.TransactionCodeID == TransactionCodeID).ToList();

            IList<ITM.Interfaces.Entities.DerivativeTransaction> result = ToList(entities);

            return result;
        }
        public IList<ITM.Interfaces.Entities.DerivativeTransaction> GetByTransactionTypeID(System.Int64? TransactionTypeID)
        {
            var entities = dbContext.DerivativeTransactions.Where(e => e.TransactionTypeID == TransactionTypeID).ToList();

            IList<ITM.Interfaces.Entities.DerivativeTransaction> result = ToList(entities);

            return result;
        }
        public IList<ITM.Interfaces.Entities.DerivativeTransaction> GetByOwnershipTypeID(System.Int64 OwnershipTypeID)
        {
            var entities = dbContext.DerivativeTransactions.Where(e => e.OwnershipTypeID == OwnershipTypeID).ToList();

            IList<ITM.Interfaces.Entities.DerivativeTransaction> result = ToList(entities);

            return result;
        }


        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.DerivativeTransaction Insert(ITM.Interfaces.Entities.DerivativeTransaction entity)
        {
            ITM.Interfaces.Entities.DerivativeTransaction result = null;
            var efEntity = Convertors.DerivativeTransactionConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.DerivativeTransaction>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.DerivativeTransactionConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.DerivativeTransaction Update(ITM.Interfaces.Entities.DerivativeTransaction entity)
        {
            ITM.Interfaces.Entities.DerivativeTransaction result = null;
            var efEntity = dbContext.DerivativeTransactions.Where(e => e.ID == entity.ID).FirstOrDefault();
            if (efEntity != null)
            {
                efEntity.Form4ReportID = entity.Form4ReportID;
                efEntity.TitleOfDerivative = entity.TitleOfDerivative;
                efEntity.ConversionExercisePrice = entity.ConversionExercisePrice;
                efEntity.TransactionDate = entity.TransactionDate;
                efEntity.TransactionCodeID = entity.TransactionCodeID;
                efEntity.EarlyVoluntarilyReport = entity.EarlyVoluntarilyReport;
                efEntity.SharesAmount = entity.SharesAmount;
                efEntity.DerivativeSecurityPrice = entity.DerivativeSecurityPrice;
                efEntity.TransactionTypeID = entity.TransactionTypeID;
                efEntity.DateExercisable = entity.DateExercisable;
                efEntity.ExpirationDate = entity.ExpirationDate;
                efEntity.UnderlyingTitle = entity.UnderlyingTitle;
                efEntity.UnderlyingSharesAmount = entity.UnderlyingSharesAmount;
                efEntity.AmountFollowingReport = entity.AmountFollowingReport;
                efEntity.OwnershipTypeID = entity.OwnershipTypeID;
                efEntity.NatureOfIndirectOwnership = entity.NatureOfIndirectOwnership;
                dbContext.SaveChanges();

                efEntity = dbContext.DerivativeTransactions.Where(e => e.ID == entity.ID).FirstOrDefault();
                result = Convertors.DerivativeTransactionConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.DerivativeTransaction> ToList(IList<ITM.DAL.EF.Models.DerivativeTransaction> entities)
        {
            IList<ITM.Interfaces.Entities.DerivativeTransaction> result = new List<ITM.Interfaces.Entities.DerivativeTransaction>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.DerivativeTransactionConvertor.FromEFEntity(e));
                }
            }
            return result;
        }

        #endregion
    }
}