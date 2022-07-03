

using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

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

        public static ITM.Interfaces.Entities.Entity EntityFromRow(DataRow row)
        {
            var entity = new ITM.Interfaces.Entities.Entity();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.EntityTypeID = !DBNull.Value.Equals(row["EntityTypeID"]) ? (System.Int64)row["EntityTypeID"] : default(System.Int64);
            entity.CIK = !DBNull.Value.Equals(row["CIK"]) ? (System.Int32)row["CIK"] : default(System.Int32);
            entity.Name = !DBNull.Value.Equals(row["Name"]) ? (System.String)row["Name"] : default(System.String);
            entity.TradingSymbol = !DBNull.Value.Equals(row["TradingSymbol"]) ? (System.String)row["TradingSymbol"] : default(System.String);
            entity.IsMonitored = !DBNull.Value.Equals(row["IsMonitored"]) ? (System.Boolean)row["IsMonitored"] : default(System.Boolean);

            return entity;
        }
    }
}
