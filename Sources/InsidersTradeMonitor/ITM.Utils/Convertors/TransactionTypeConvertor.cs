





using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class TransactionTypeConvertor
    {
        public static DTO.TransactionType Convert(Interfaces.Entities.TransactionType entity, IUrlHelper url)
        {
            var dto = new DTO.TransactionType()
            {
        		        ID = entity.ID,

				        Code = entity.Code,

				        Description = entity.Description,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetTransactionType", "transactiontypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteTransactionType", "transactiontypes", new { id = dto.ID  }), "delete_transactiontype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertTransactionType", "transactiontypes"), "insert_transactiontype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateTransactionType", "transactiontypes"), "update_transactiontype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.TransactionType Convert(DTO.TransactionType dto)
        {
            var entity = new Interfaces.Entities.TransactionType()
            {
                
        		        ID = dto.ID,

				        Code = dto.Code,

				        Description = dto.Description,

				
     
            };

            return entity;
        }
    }
}
