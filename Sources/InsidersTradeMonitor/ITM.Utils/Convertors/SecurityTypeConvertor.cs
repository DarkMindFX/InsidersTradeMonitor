





using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class SecurityTypeConvertor
    {
        public static DTO.SecurityType Convert(Interfaces.Entities.SecurityType entity, IUrlHelper url)
        {
            var dto = new DTO.SecurityType()
            {
        		        ID = entity.ID,

				        SecurityTypeName = entity.SecurityTypeName,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetSecurityType", "securitytypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteSecurityType", "securitytypes", new { id = dto.ID  }), "delete_securitytype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertSecurityType", "securitytypes"), "insert_securitytype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateSecurityType", "securitytypes"), "update_securitytype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.SecurityType Convert(DTO.SecurityType dto)
        {
            var entity = new Interfaces.Entities.SecurityType()
            {
                
        		        ID = dto.ID,

				        SecurityTypeName = dto.SecurityTypeName,

				
     
            };

            return entity;
        }
    }
}
