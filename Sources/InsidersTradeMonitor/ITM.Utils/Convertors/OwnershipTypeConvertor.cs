





using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ITM.Utils.Convertors
{
    public class OwnershipTypeConvertor
    {
        public static ITM.DTO.OwnershipType Convert(ITM.Interfaces.Entities.OwnershipType entity, IUrlHelper url)
        {
            var dto = new ITM.DTO.OwnershipType()
            {
                ID = entity.ID,

                Code = entity.Code,

                Description = entity.Description,


            };

            if (url != null)
            {
                dto.Links.Add(new ITM.DTO.Link(url.Action("GetOwnershipType", "ownershiptypes", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("DeleteOwnershipType", "ownershiptypes", new { id = dto.ID }), "delete_ownershiptype", "DELETE"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("InsertOwnershipType", "ownershiptypes"), "insert_ownershiptype", "POST"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("UpdateOwnershipType", "ownershiptypes"), "update_ownershiptype", "PUT"));
            }
            return dto;

        }

        public static ITM.Interfaces.Entities.OwnershipType Convert(ITM.DTO.OwnershipType dto)
        {
            var entity = new ITM.Interfaces.Entities.OwnershipType()
            {

                ID = dto.ID,

                Code = dto.Code,

                Description = dto.Description,
            };

            return entity;
        }

        public static ITM.Interfaces.Entities.OwnershipType OwnershipTypeFromRow(DataRow row)
        {
            var entity = new ITM.Interfaces.Entities.OwnershipType();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.Code = !DBNull.Value.Equals(row["Code"]) ? (System.String)row["Code"] : default(System.String);
            entity.Description = !DBNull.Value.Equals(row["Description"]) ? (System.String)row["Description"] : default(System.String);

            return entity;
        }
    }
}
