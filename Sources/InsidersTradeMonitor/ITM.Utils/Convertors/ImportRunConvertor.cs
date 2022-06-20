

using Microsoft.AspNetCore.Mvc;
using System;

namespace ITM.Utils.Convertors
{
    public class ImportRunConvertor
    {
        public static DTO.ImportRun Convert(Interfaces.Entities.ImportRun entity, IUrlHelper url)
        {
            var dto = new DTO.ImportRun()
            {
        		        ID = entity.ID,

				        TimeStart = entity.TimeStart,

				        TimeEnd = entity.TimeEnd,

				        RequestJson = entity.RequestJson,

				        StateID = entity.StateID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetImportRun", "importruns", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteImportRun", "importruns", new { id = dto.ID  }), "delete_importrun", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertImportRun", "importruns"), "insert_importrun", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateImportRun", "importruns"), "update_importrun", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.ImportRun Convert(DTO.ImportRun dto)
        {
            var entity = new Interfaces.Entities.ImportRun()
            {
                
        		        ID = dto.ID,

				        TimeStart = dto.TimeStart,

				        TimeEnd = dto.TimeEnd,

				        RequestJson = dto.RequestJson,

				        StateID = dto.StateID,

				
     
            };

            return entity;
        }
    }
}
