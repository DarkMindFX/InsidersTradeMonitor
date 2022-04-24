





using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class DerivativeTransactionConvertor
    {
        public static DTO.DerivativeTransaction Convert(Interfaces.Entities.DerivativeTransaction entity, IUrlHelper url)
        {
            var dto = new DTO.DerivativeTransaction()
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

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetDerivativeTransaction", "derivativetransactions", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteDerivativeTransaction", "derivativetransactions", new { id = dto.ID  }), "delete_derivativetransaction", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertDerivativeTransaction", "derivativetransactions"), "insert_derivativetransaction", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateDerivativeTransaction", "derivativetransactions"), "update_derivativetransaction", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.DerivativeTransaction Convert(DTO.DerivativeTransaction dto)
        {
            var entity = new Interfaces.Entities.DerivativeTransaction()
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
    }
}
