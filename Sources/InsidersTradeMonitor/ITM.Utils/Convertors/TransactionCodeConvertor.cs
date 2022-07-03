
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ITM.Utils.Convertors
{
    public class TransactionCodeConvertor
    {
        public static ITM.DTO.TransactionCode Convert(ITM.Interfaces.Entities.TransactionCode entity, IUrlHelper url)
        {
            var dto = new ITM.DTO.TransactionCode()
            {
                ID = entity.ID,

                Code = entity.Code,

                Description = entity.Description,
            };

            if (url != null)
            {
                dto.Links.Add(new ITM.DTO.Link(url.Action("GetTransactionCode", "transactioncodes", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("DeleteTransactionCode", "transactioncodes", new { id = dto.ID }), "delete_transactioncode", "DELETE"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("InsertTransactionCode", "transactioncodes"), "insert_transactioncode", "POST"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("UpdateTransactionCode", "transactioncodes"), "update_transactioncode", "PUT"));
            }
            return dto;

        }

        public static ITM.Interfaces.Entities.TransactionCode Convert(ITM.DTO.TransactionCode dto)
        {
            var entity = new ITM.Interfaces.Entities.TransactionCode()
            {

                ID = dto.ID,

                Code = dto.Code,

                Description = dto.Description,
            };

            return entity;
        }

        public static ITM.Interfaces.Entities.TransactionCode TransactionCodeFromRow(DataRow row)
        {
            var entity = new ITM.Interfaces.Entities.TransactionCode();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.Code = !DBNull.Value.Equals(row["Code"]) ? (System.String)row["Code"] : default(System.String);
            entity.Description = !DBNull.Value.Equals(row["Description"]) ? (System.String)row["Description"] : default(System.String);

            return entity;
        }
    }
}
