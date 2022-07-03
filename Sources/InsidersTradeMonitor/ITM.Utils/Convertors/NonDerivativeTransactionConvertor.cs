
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ITM.Utils.Convertors
{
    public class NonDerivativeTransactionConvertor
    {
        public static ITM.DTO.NonDerivativeTransaction Convert(ITM.Interfaces.Entities.NonDerivativeTransaction entity, IUrlHelper url)
        {
            var dto = new ITM.DTO.NonDerivativeTransaction()
            {
                ID = entity.ID,

                Form4ReportID = entity.Form4ReportID,

                TitleOfSecurity = entity.TitleOfSecurity,

                TransactionDate = entity.TransactionDate,

                DeemedExecDate = entity.DeemedExecDate,

                TransactionCodeID = entity.TransactionCodeID,

                EarlyVoluntarilyReport = entity.EarlyVoluntarilyReport,

                SharesAmount = entity.SharesAmount,

                TransactionTypeID = entity.TransactionTypeID,

                Price = entity.Price,

                AmountFollowingReport = entity.AmountFollowingReport,

                OwnershipTypeID = entity.OwnershipTypeID,

                NatureOfIndirectOwnership = entity.NatureOfIndirectOwnership,


            };

            if (url != null)
            {
                dto.Links.Add(new ITM.DTO.Link(url.Action("GetNonDerivativeTransaction", "nonderivativetransactions", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("DeleteNonDerivativeTransaction", "nonderivativetransactions", new { id = dto.ID }), "delete_nonderivativetransaction", "DELETE"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("InsertNonDerivativeTransaction", "nonderivativetransactions"), "insert_nonderivativetransaction", "POST"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("UpdateNonDerivativeTransaction", "nonderivativetransactions"), "update_nonderivativetransaction", "PUT"));
            }
            return dto;

        }

        public static ITM.Interfaces.Entities.NonDerivativeTransaction Convert(ITM.DTO.NonDerivativeTransaction dto)
        {
            var entity = new ITM.Interfaces.Entities.NonDerivativeTransaction()
            {

                ID = dto.ID,

                Form4ReportID = dto.Form4ReportID,

                TitleOfSecurity = dto.TitleOfSecurity,

                TransactionDate = dto.TransactionDate,

                DeemedExecDate = dto.DeemedExecDate,

                TransactionCodeID = dto.TransactionCodeID,

                EarlyVoluntarilyReport = dto.EarlyVoluntarilyReport,

                SharesAmount = dto.SharesAmount,

                TransactionTypeID = dto.TransactionTypeID,

                Price = dto.Price,

                AmountFollowingReport = dto.AmountFollowingReport,

                OwnershipTypeID = dto.OwnershipTypeID,

                NatureOfIndirectOwnership = dto.NatureOfIndirectOwnership,

            };

            return entity;
        }

        public static ITM.Interfaces.Entities.NonDerivativeTransaction NonDerivativeTransactionFromRow(DataRow row)
        {
            var entity = new ITM.Interfaces.Entities.NonDerivativeTransaction();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.Form4ReportID = !DBNull.Value.Equals(row["Form4ReportID"]) ? (System.Int64)row["Form4ReportID"] : default(System.Int64);
            entity.TitleOfSecurity = !DBNull.Value.Equals(row["TitleOfSecurity"]) ? (System.String)row["TitleOfSecurity"] : default(System.String);
            entity.TransactionDate = !DBNull.Value.Equals(row["TransactionDate"]) ? (System.DateTime)row["TransactionDate"] : default(System.DateTime);
            entity.DeemedExecDate = !DBNull.Value.Equals(row["DeemedExecDate"]) ? (System.DateTime?)row["DeemedExecDate"] : default(System.DateTime?);
            entity.TransactionCodeID = !DBNull.Value.Equals(row["TransactionCodeID"]) ? (System.Int64)row["TransactionCodeID"] : default(System.Int64);
            entity.EarlyVoluntarilyReport = !DBNull.Value.Equals(row["EarlyVoluntarilyReport"]) ? (System.Boolean)row["EarlyVoluntarilyReport"] : default(System.Boolean);
            entity.SharesAmount = !DBNull.Value.Equals(row["SharesAmount"]) ? (System.Int64?)row["SharesAmount"] : default(System.Int64?);
            entity.TransactionTypeID = !DBNull.Value.Equals(row["TransactionTypeID"]) ? (System.Int64)row["TransactionTypeID"] : default(System.Int64);
            entity.Price = !DBNull.Value.Equals(row["Price"]) ? (System.Decimal)row["Price"] : default(System.Decimal);
            entity.AmountFollowingReport = !DBNull.Value.Equals(row["AmountFollowingReport"]) ? (System.Int64)row["AmountFollowingReport"] : default(System.Int64);
            entity.OwnershipTypeID = !DBNull.Value.Equals(row["OwnershipTypeID"]) ? (System.Int64)row["OwnershipTypeID"] : default(System.Int64);
            entity.NatureOfIndirectOwnership = !DBNull.Value.Equals(row["NatureOfIndirectOwnership"]) ? (System.String)row["NatureOfIndirectOwnership"] : default(System.String);

            return entity;
        }
    }
}
