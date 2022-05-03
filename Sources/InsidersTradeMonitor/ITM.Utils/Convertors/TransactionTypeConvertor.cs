

using Microsoft.AspNetCore.Mvc;
using System;

namespace ITM.Utils.Convertors
{
    public class TransactionTypeConvertor
    {
        public static ITM.DTO.TransactionType Convert(ITM.Interfaces.Entities.TransactionType entity, IUrlHelper url)
        {
            var dto = new ITM.DTO.TransactionType()
            {
                ID = entity.ID,

                Code = entity.Code,

                Description = entity.Description,


            };

            if (url != null)
            {
                dto.Links.Add(new ITM.DTO.Link(url.Action("GetTransactionType", "transactiontypes", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("DeleteTransactionType", "transactiontypes", new { id = dto.ID }), "delete_transactiontype", "DELETE"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("InsertTransactionType", "transactiontypes"), "insert_transactiontype", "POST"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("UpdateTransactionType", "transactiontypes"), "update_transactiontype", "PUT"));
            }
            return dto;

        }

        public static ITM.Interfaces.Entities.TransactionType Convert(ITM.DTO.TransactionType dto)
        {
            var entity = new ITM.Interfaces.Entities.TransactionType()
            {

                ID = dto.ID,

                Code = dto.Code,

                Description = dto.Description,
            };

            return entity;
        }
    }
}
