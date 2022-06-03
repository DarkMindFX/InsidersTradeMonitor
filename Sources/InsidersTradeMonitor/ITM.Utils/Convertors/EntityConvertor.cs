

using Microsoft.AspNetCore.Mvc;
using System;

namespace ITM.Utils.Convertors
{
    public class EntityConvertor
    {
        public static DTO.Entity Convert(Interfaces.Entities.Entity entity, IUrlHelper url)
        {
            var dto = new DTO.Entity()
            {
                ID = entity.ID,

                EntityTypeID = entity.EntityTypeID,

                CIK = entity.CIK,

                Name = entity.Name,

                TradingSymbol = entity.TradingSymbol,

                IsMonitored = entity.IsMonitored,


            };

            if (url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetEntity", "entities", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteEntity", "entities", new { id = dto.ID }), "delete_entity", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertEntity", "entities"), "insert_entity", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateEntity", "entities"), "update_entity", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Entity Convert(DTO.Entity dto)
        {
            var entity = new Interfaces.Entities.Entity()
            {

                ID = dto.ID,

                EntityTypeID = dto.EntityTypeID,

                CIK = dto.CIK,

                Name = dto.Name,

                TradingSymbol = dto.TradingSymbol,

                IsMonitored = dto.IsMonitored,



            };

            return entity;
        }
    }
}
