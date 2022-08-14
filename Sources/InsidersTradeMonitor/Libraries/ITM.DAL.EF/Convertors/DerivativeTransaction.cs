


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
    public class DerivativeTransactionConvertor
    {

        public static ITM.Interfaces.Entities.DerivativeTransaction FromEFEntity(ITM.DAL.EF.Models.DerivativeTransaction efEntity)
        {
            ITM.Interfaces.Entities.DerivativeTransaction result = new Interfaces.Entities.DerivativeTransaction()
            {
                ID = efEntity.ID,
                Form4ReportID = efEntity.Form4ReportID,
                TitleOfDerivative = efEntity.TitleOfDerivative,
                ConversionExercisePrice = efEntity.ConversionExercisePrice,
                TransactionDate = efEntity.TransactionDate,
                TransactionCodeID = efEntity.TransactionCodeID,
                EarlyVoluntarilyReport = efEntity.EarlyVoluntarilyReport,
                SharesAmount = efEntity.SharesAmount,
                DerivativeSecurityPrice = efEntity.DerivativeSecurityPrice,
                TransactionTypeID = efEntity.TransactionTypeID,
                DateExercisable = efEntity.DateExercisable,
                ExpirationDate = efEntity.ExpirationDate,
                UnderlyingTitle = efEntity.UnderlyingTitle,
                UnderlyingSharesAmount = efEntity.UnderlyingSharesAmount,
                AmountFollowingReport = efEntity.AmountFollowingReport,
                OwnershipTypeID = efEntity.OwnershipTypeID,
                NatureOfIndirectOwnership = efEntity.NatureOfIndirectOwnership,
            };

            return result;
        }

        public static ITM.DAL.EF.Models.DerivativeTransaction ToEFEntity(ITM.Interfaces.Entities.DerivativeTransaction entity)
        {
            ITM.DAL.EF.Models.DerivativeTransaction result = new ITM.DAL.EF.Models.DerivativeTransaction()
            {
                ID = entity.ID,
                Form4ReportID = entity.Form4ReportID,
                TitleOfDerivative = entity.TitleOfDerivative,
                ConversionExercisePrice = entity.ConversionExercisePrice,
                TransactionDate = entity.TransactionDate,
                TransactionCodeID = entity.TransactionCodeID,
                EarlyVoluntarilyReport = entity.EarlyVoluntarilyReport,
                UnderlyingTitle = entity.UnderlyingTitle,
                UnderlyingSharesAmount = entity.UnderlyingSharesAmount,
                AmountFollowingReport = entity.AmountFollowingReport,
                OwnershipTypeID = entity.OwnershipTypeID,
            };

            if (entity.SharesAmount.HasValue)
            {
                result.SharesAmount = (System.Int64)entity.SharesAmount;
            }
            if (entity.DerivativeSecurityPrice.HasValue)
            {
                result.DerivativeSecurityPrice = (System.Decimal)entity.DerivativeSecurityPrice;
            }
            if (entity.TransactionTypeID.HasValue)
            {
                result.TransactionTypeID = (System.Int64)entity.TransactionTypeID;
            }
            if (entity.DateExercisable.HasValue)
            {
                result.DateExercisable = (System.DateTime)entity.DateExercisable;
            }
            if (entity.ExpirationDate.HasValue)
            {
                result.ExpirationDate = (System.DateTime)entity.ExpirationDate;
            }
            if (entity.NatureOfIndirectOwnership.HasValue)
            {
                result.NatureOfIndirectOwnership = (System.String)entity.NatureOfIndirectOwnership;
            }

            return result;
        }
    }

}