





using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class EntityTypeConvertor
    {
        public static DTO.EntityType Convert(Interfaces.Entities.EntityType entity, IUrlHelper url)
        {
            var dto = new DTO.EntityType()
            {
        		        ID = entity.ID,

				        TypeName = entity.TypeName,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetEntityType", "entitytypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteEntityType", "entitytypes", new { id = dto.ID  }), "delete_entitytype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertEntityType", "entitytypes"), "insert_entitytype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateEntityType", "entitytypes"), "update_entitytype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.EntityType Convert(DTO.EntityType dto)
        {
            var entity = new Interfaces.Entities.EntityType()
            {
                
        		        ID = dto.ID,

				        TypeName = dto.TypeName,

				
     
            };

            return entity;
        }
    }
}
