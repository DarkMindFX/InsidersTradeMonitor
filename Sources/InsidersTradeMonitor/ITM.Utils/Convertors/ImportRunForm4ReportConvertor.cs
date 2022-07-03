

using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ITM.Utils.Convertors
{
    public class ImportRunForm4ReportConvertor
    {
        public static DTO.ImportRunForm4Report Convert(Interfaces.Entities.ImportRunForm4Report entity, IUrlHelper url)
        {
            var dto = new DTO.ImportRunForm4Report()
            {
        		        ID = entity.ID,

				        ImportRunID = entity.ImportRunID,

				        Form4ReportID = entity.Form4ReportID,

				        TimeStarted = entity.TimeStarted,

				        TimeCompleted = entity.TimeCompleted,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetImportRunForm4Report", "importrunform4reports", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteImportRunForm4Report", "importrunform4reports", new { id = dto.ID  }), "delete_importrunform4report", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertImportRunForm4Report", "importrunform4reports"), "insert_importrunform4report", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateImportRunForm4Report", "importrunform4reports"), "update_importrunform4report", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.ImportRunForm4Report Convert(DTO.ImportRunForm4Report dto)
        {
            var entity = new Interfaces.Entities.ImportRunForm4Report()
            {
                
        		        ID = dto.ID,

				        ImportRunID = dto.ImportRunID,

				        Form4ReportID = dto.Form4ReportID,

				        TimeStarted = dto.TimeStarted,

				        TimeCompleted = dto.TimeCompleted,

				
     
            };

            return entity;
        }

        public static ITM.Interfaces.Entities.ImportRunForm4Report ImportRunForm4ReportFromRow(DataRow row)
        {
            var entity = new ITM.Interfaces.Entities.ImportRunForm4Report();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.ImportRunID = !DBNull.Value.Equals(row["ImportRunID"]) ? (System.Int64)row["ImportRunID"] : default(System.Int64);
            entity.Form4ReportID = !DBNull.Value.Equals(row["Form4ReportID"]) ? (System.Int64)row["Form4ReportID"] : default(System.Int64);
            entity.TimeStarted = !DBNull.Value.Equals(row["TimeStarted"]) ? (System.DateTime)row["TimeStarted"] : default(System.DateTime);
            entity.TimeCompleted = !DBNull.Value.Equals(row["TimeCompleted"]) ? (System.DateTime?)row["TimeCompleted"] : default(System.DateTime?);

            return entity;
        }
    }
}
