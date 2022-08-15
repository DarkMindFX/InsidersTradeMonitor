


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
    public class NonDerivativeTransactionConvertor
    {

        public static ITM.Interfaces.Entities.NonDerivativeTransaction FromEFEntity(ITM.DAL.EF.Models.NonDerivativeTransaction efEntity)
        {
            ITM.Interfaces.Entities.NonDerivativeTransaction result = new Interfaces.Entities.NonDerivativeTransaction()
            {
                ID = efEntity.ID,
                Form4ReportID = efEntity.Form4ReportID,
                TitleOfSecurity = efEntity.TitleOfSecurity,
                TransactionDate = efEntity.TransactionDate,
                DeemedExecDate = efEntity.DeemedExecDate,
                TransactionCodeID = efEntity.TransactionCodeID,
                EarlyVoluntarilyReport = efEntity.EarlyVoluntarilyReport,
                SharesAmount = efEntity.SharesAmount,
                TransactionTypeID = efEntity.TransactionTypeID,
                Price = efEntity.Price,
                AmountFollowingReport = efEntity.AmountFollowingReport,
                OwnershipTypeID = efEntity.OwnershipTypeID,
                NatureOfIndirectOwnership = efEntity.NatureOfIndirectOwnership,
            };

            return result;
        }

        public static ITM.DAL.EF.Models.NonDerivativeTransaction ToEFEntity(ITM.Interfaces.Entities.NonDerivativeTransaction entity)
        {
            ITM.DAL.EF.Models.NonDerivativeTransaction result = new ITM.DAL.EF.Models.NonDerivativeTransaction()
            {
                ID = entity.ID,
                Form4ReportID = entity.Form4ReportID,
                TitleOfSecurity = entity.TitleOfSecurity,
                TransactionDate = entity.TransactionDate,
                EarlyVoluntarilyReport = entity.EarlyVoluntarilyReport,
                Price = entity.Price,
                AmountFollowingReport = entity.AmountFollowingReport,
            };

            if (entity.DeemedExecDate.HasValue)
            {
                result.DeemedExecDate = (System.DateTime)entity.DeemedExecDate;
            }
            if (entity.TransactionCodeID.HasValue)
            {
                result.TransactionCodeID = (System.Int64)entity.TransactionCodeID;
            }
            if (entity.SharesAmount.HasValue)
            {
                result.SharesAmount = (System.Int64)entity.SharesAmount;
            }
            if (entity.TransactionTypeID.HasValue)
            {
                result.TransactionTypeID = (System.Int64)entity.TransactionTypeID;
            }
            if (entity.OwnershipTypeID.HasValue)
            {
                result.OwnershipTypeID = (System.Int64)entity.OwnershipTypeID;
            }
            if (entity.NatureOfIndirectOwnership != null)
            {
                result.NatureOfIndirectOwnership = (System.String)entity.NatureOfIndirectOwnership;
            }

            return result;
        }
    }

}