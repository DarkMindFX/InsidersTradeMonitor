

using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ITM.Utils.Convertors
{
    public class ImportRunStateConvertor
    {
        public static DTO.ImportRunState Convert(Interfaces.Entities.ImportRunState entity, IUrlHelper url)
        {
            var dto = new DTO.ImportRunState()
            {
                ID = entity.ID,

                Name = entity.Name,


            };

            if (url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetImportRunState", "importrunstates", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteImportRunState", "importrunstates", new { id = dto.ID }), "delete_importrunstate", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertImportRunState", "importrunstates"), "insert_importrunstate", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateImportRunState", "importrunstates"), "update_importrunstate", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.ImportRunState Convert(DTO.ImportRunState dto)
        {
            var entity = new Interfaces.Entities.ImportRunState()
            {

                ID = dto.ID,

                Name = dto.Name,
            };

            return entity;
        }

        public static ITM.Interfaces.Entities.ImportRunState ImportRunStateFromRow(DataRow row)
        {
            var entity = new ITM.Interfaces.Entities.ImportRunState();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.Name = !DBNull.Value.Equals(row["Name"]) ? (System.String)row["Name"] : default(System.String);

            return entity;
        }
    }
}
