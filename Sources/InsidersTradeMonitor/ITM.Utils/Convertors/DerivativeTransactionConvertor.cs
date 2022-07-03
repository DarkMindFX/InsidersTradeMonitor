

using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ITM.Utils.Convertors
{
    public class DerivativeTransactionConvertor
    {
        public static ITM.DTO.DerivativeTransaction Convert(ITM.Interfaces.Entities.DerivativeTransaction entity, IUrlHelper url)
        {
            var dto = new ITM.DTO.DerivativeTransaction()
            {
                ID = entity.ID,

                Form4ReportID = entity.Form4ReportID,

                TitleOfDerivative = entity.TitleOfDerivative,

                ConversionExercisePrice = entity.ConversionExercisePrice,

                TransactionDate = entity.TransactionDate,

                TransactionCodeID = entity.TransactionCodeID,

                EarlyVoluntarilyReport = entity.EarlyVoluntarilyReport,

                SharesAmount = entity.SharesAmount,

                DerivativeSecurityPrice = entity.DerivativeSecurityPrice,

                TransactionTypeID = entity.TransactionTypeID,

                DateExercisable = entity.DateExercisable,

                ExpirationDate = entity.ExpirationDate,

                UnderlyingTitle = entity.UnderlyingTitle,

                UnderlyingSharesAmount = entity.UnderlyingSharesAmount,

                AmountFollowingReport = entity.AmountFollowingReport,

                OwnershipTypeID = entity.OwnershipTypeID,

                NatureOfIndirectOwnership = entity.NatureOfIndirectOwnership,


            };

            if (url != null)
            {
                dto.Links.Add(new ITM.DTO.Link(url.Action("GetDerivativeTransaction", "derivativetransactions", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("DeleteDerivativeTransaction", "derivativetransactions", new { id = dto.ID }), "delete_derivativetransaction", "DELETE"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("InsertDerivativeTransaction", "derivativetransactions"), "insert_derivativetransaction", "POST"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("UpdateDerivativeTransaction", "derivativetransactions"), "update_derivativetransaction", "PUT"));
            }
            return dto;

        }

        public static ITM.Interfaces.Entities.DerivativeTransaction Convert(ITM.DTO.DerivativeTransaction dto)
        {
            var entity = new ITM.Interfaces.Entities.DerivativeTransaction()
            {

                ID = dto.ID,

                Form4ReportID = dto.Form4ReportID,

                TitleOfDerivative = dto.TitleOfDerivative,

                ConversionExercisePrice = dto.ConversionExercisePrice,

                TransactionDate = dto.TransactionDate,

                TransactionCodeID = dto.TransactionCodeID,

                EarlyVoluntarilyReport = dto.EarlyVoluntarilyReport,

                SharesAmount = dto.SharesAmount,

                DerivativeSecurityPrice = dto.DerivativeSecurityPrice,

                TransactionTypeID = dto.TransactionTypeID,

                DateExercisable = dto.DateExercisable,

                ExpirationDate = dto.ExpirationDate,

                UnderlyingTitle = dto.UnderlyingTitle,

                UnderlyingSharesAmount = dto.UnderlyingSharesAmount,

                AmountFollowingReport = dto.AmountFollowingReport,

                OwnershipTypeID = dto.OwnershipTypeID,

                NatureOfIndirectOwnership = dto.NatureOfIndirectOwnership,



            };

            return entity;
        }

        public static ITM.Interfaces.Entities.DerivativeTransaction DerivativeTransactionFromRow(DataRow row)
        {
            var entity = new ITM.Interfaces.Entities.DerivativeTransaction();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.Form4ReportID = !DBNull.Value.Equals(row["Form4ReportID"]) ? (System.Int64)row["Form4ReportID"] : default(System.Int64);
            entity.TitleOfDerivative = !DBNull.Value.Equals(row["TitleOfDerivative"]) ? (System.String)row["TitleOfDerivative"] : default(System.String);
            entity.ConversionExercisePrice = !DBNull.Value.Equals(row["ConversionExercisePrice"]) ? (System.Decimal)row["ConversionExercisePrice"] : default(System.Decimal);
            entity.TransactionDate = !DBNull.Value.Equals(row["TransactionDate"]) ? (System.DateTime)row["TransactionDate"] : default(System.DateTime);
            entity.TransactionCodeID = !DBNull.Value.Equals(row["TransactionCodeID"]) ? (System.Int64)row["TransactionCodeID"] : default(System.Int64);
            entity.EarlyVoluntarilyReport = !DBNull.Value.Equals(row["EarlyVoluntarilyReport"]) ? (System.Boolean)row["EarlyVoluntarilyReport"] : default(System.Boolean);
            entity.SharesAmount = !DBNull.Value.Equals(row["SharesAmount"]) ? (System.Int64?)row["SharesAmount"] : default(System.Int64?);
            entity.DerivativeSecurityPrice = !DBNull.Value.Equals(row["DerivativeSecurityPrice"]) ? (System.Decimal?)row["DerivativeSecurityPrice"] : default(System.Decimal?);
            entity.TransactionTypeID = !DBNull.Value.Equals(row["TransactionTypeID"]) ? (System.Int64?)row["TransactionTypeID"] : default(System.Int64?);
            entity.DateExercisable = !DBNull.Value.Equals(row["DateExercisable"]) ? (System.DateTime?)row["DateExercisable"] : default(System.DateTime?);
            entity.ExpirationDate = !DBNull.Value.Equals(row["ExpirationDate"]) ? (System.DateTime?)row["ExpirationDate"] : default(System.DateTime?);
            entity.UnderlyingTitle = !DBNull.Value.Equals(row["UnderlyingTitle"]) ? (System.String)row["UnderlyingTitle"] : default(System.String);
            entity.UnderlyingSharesAmount = !DBNull.Value.Equals(row["UnderlyingSharesAmount"]) ? (System.Int64)row["UnderlyingSharesAmount"] : default(System.Int64);
            entity.AmountFollowingReport = !DBNull.Value.Equals(row["AmountFollowingReport"]) ? (System.Int64)row["AmountFollowingReport"] : default(System.Int64);
            entity.OwnershipTypeID = !DBNull.Value.Equals(row["OwnershipTypeID"]) ? (System.Int64)row["OwnershipTypeID"] : default(System.Int64);
            entity.NatureOfIndirectOwnership = !DBNull.Value.Equals(row["NatureOfIndirectOwnership"]) ? (System.String)row["NatureOfIndirectOwnership"] : default(System.String);

            return entity;
        }
    }
}
