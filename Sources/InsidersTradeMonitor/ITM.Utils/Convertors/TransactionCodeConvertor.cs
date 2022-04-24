





using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class TransactionCodeConvertor
    {
        public static DTO.TransactionCode Convert(Interfaces.Entities.TransactionCode entity, IUrlHelper url)
        {
            var dto = new DTO.TransactionCode()
            {
        		        ID = entity.ID,

				        Code = entity.Code,

				        Description = entity.Description,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetTransactionCode", "transactioncodes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteTransactionCode", "transactioncodes", new { id = dto.ID  }), "delete_transactioncode", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertTransactionCode", "transactioncodes"), "insert_transactioncode", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateTransactionCode", "transactioncodes"), "update_transactioncode", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.TransactionCode Convert(DTO.TransactionCode dto)
        {
            var entity = new Interfaces.Entities.TransactionCode()
            {
                
        		        ID = dto.ID,

				        Code = dto.Code,

				        Description = dto.Description,

				
     
            };

            return entity;
        }
    }
}
