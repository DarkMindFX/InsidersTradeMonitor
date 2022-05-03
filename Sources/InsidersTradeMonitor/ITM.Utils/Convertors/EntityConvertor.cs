
using Microsoft.AspNetCore.Mvc;
using System;

namespace ITM.Utils.Convertors
{
    public class EntityConvertor
    {
        public static ITM.DTO.Entity Convert(ITM.Interfaces.Entities.Entity entity, IUrlHelper url)
        {
            var dto = new ITM.DTO.Entity()
            {
                ID = entity.ID,

                EntityTypeID = entity.EntityTypeID,

                CIK = entity.CIK,

                Name = entity.Name,

                TradingSymbol = entity.TradingSymbol,


            };

            if (url != null)
            {
                dto.Links.Add(new ITM.DTO.Link(url.Action("GetEntity", "entities", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("DeleteEntity", "entities", new { id = dto.ID }), "delete_entity", "DELETE"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("InsertEntity", "entities"), "insert_entity", "POST"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("UpdateEntity", "entities"), "update_entity", "PUT"));
            }
            return dto;

        }

        public static ITM.Interfaces.Entities.Entity Convert(ITM.DTO.Entity dto)
        {
            var entity = new ITM.Interfaces.Entities.Entity()
            {

                ID = dto.ID,

                EntityTypeID = dto.EntityTypeID,

                CIK = dto.CIK,

                Name = dto.Name,

                TradingSymbol = dto.TradingSymbol,
            };

            return entity;
        }
    }
}
