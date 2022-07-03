

using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ITM.Utils.Convertors
{
    public class UserConvertor
    {
        public static DTO.User Convert(Interfaces.Entities.User entity, IUrlHelper url)
        {
            var dto = new DTO.User()
            {
        		        ID = entity.ID,

				        Login = entity.Login,

				        Salt = entity.Salt,

				        FirstName = entity.FirstName,

				        MiddleName = entity.MiddleName,

				        LastName = entity.LastName,

				        FriendlyName = entity.FriendlyName,

				        CreatedDate = entity.CreatedDate,

				        ModifiedDate = entity.ModifiedDate,

				        ModifiedByID = entity.ModifiedByID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetUser", "users", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteUser", "users", new { id = dto.ID  }), "delete_user", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertUser", "users"), "insert_user", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateUser", "users"), "update_user", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.User Convert(DTO.User dto)
        {
            var entity = new Interfaces.Entities.User()
            {
                
        		        ID = dto.ID,

				        Login = dto.Login,

				        Salt = dto.Salt,

				        FirstName = dto.FirstName,

				        MiddleName = dto.MiddleName,

				        LastName = dto.LastName,

				        FriendlyName = dto.FriendlyName,

				        CreatedDate = dto.CreatedDate,

				        ModifiedDate = dto.ModifiedDate,

				        ModifiedByID = dto.ModifiedByID,
     
            };

            return entity;
        }

        public static ITM.Interfaces.Entities.User UserFromRow(DataRow row)
        {
            var entity = new ITM.Interfaces.Entities.User();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.Login = !DBNull.Value.Equals(row["Login"]) ? (System.String)row["Login"] : default(System.String);
            entity.PwdHash = !DBNull.Value.Equals(row["PwdHash"]) ? (System.String)row["PwdHash"] : default(System.String);
            entity.Salt = !DBNull.Value.Equals(row["Salt"]) ? (System.String)row["Salt"] : default(System.String);
            entity.FirstName = !DBNull.Value.Equals(row["FirstName"]) ? (System.String)row["FirstName"] : default(System.String);
            entity.MiddleName = !DBNull.Value.Equals(row["MiddleName"]) ? (System.String)row["MiddleName"] : default(System.String);
            entity.LastName = !DBNull.Value.Equals(row["LastName"]) ? (System.String)row["LastName"] : default(System.String);
            entity.FriendlyName = !DBNull.Value.Equals(row["FriendlyName"]) ? (System.String)row["FriendlyName"] : default(System.String);
            entity.CreatedDate = !DBNull.Value.Equals(row["CreatedDate"]) ? (System.DateTime)row["CreatedDate"] : default(System.DateTime);
            entity.ModifiedDate = !DBNull.Value.Equals(row["ModifiedDate"]) ? (System.DateTime?)row["ModifiedDate"] : default(System.DateTime?);
            entity.ModifiedByID = !DBNull.Value.Equals(row["ModifiedByID"]) ? (System.Int64?)row["ModifiedByID"] : default(System.Int64?);

            return entity;
        }
    }
}
