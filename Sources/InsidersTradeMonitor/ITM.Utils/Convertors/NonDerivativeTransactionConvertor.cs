





using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class NonDerivativeTransactionConvertor
    {
        public static DTO.NonDerivativeTransaction Convert(Interfaces.Entities.NonDerivativeTransaction entity, IUrlHelper url)
        {
            var dto = new DTO.NonDerivativeTransaction()
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

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetNonDerivativeTransaction", "nonderivativetransactions", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteNonDerivativeTransaction", "nonderivativetransactions", new { id = dto.ID  }), "delete_nonderivativetransaction", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertNonDerivativeTransaction", "nonderivativetransactions"), "insert_nonderivativetransaction", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateNonDerivativeTransaction", "nonderivativetransactions"), "update_nonderivativetransaction", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.NonDerivativeTransaction Convert(DTO.NonDerivativeTransaction dto)
        {
            var entity = new Interfaces.Entities.NonDerivativeTransaction()
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
    }
}
