





using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class OwnershipTypeConvertor
    {
        public static DTO.OwnershipType Convert(Interfaces.Entities.OwnershipType entity, IUrlHelper url)
        {
            var dto = new DTO.OwnershipType()
            {
        		        ID = entity.ID,

				        Code = entity.Code,

				        Description = entity.Description,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetOwnershipType", "ownershiptypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteOwnershipType", "ownershiptypes", new { id = dto.ID  }), "delete_ownershiptype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertOwnershipType", "ownershiptypes"), "insert_ownershiptype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateOwnershipType", "ownershiptypes"), "update_ownershiptype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.OwnershipType Convert(DTO.OwnershipType dto)
        {
            var entity = new Interfaces.Entities.OwnershipType()
            {
                
        		        ID = dto.ID,

				        Code = dto.Code,

				        Description = dto.Description,

				
     
            };

            return entity;
        }
    }
}
