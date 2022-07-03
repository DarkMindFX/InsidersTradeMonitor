

using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ITM.Utils.Convertors
{
    public class Form4ReportConvertor
    {
        public static DTO.Form4Report Convert(Interfaces.Entities.Form4Report entity, IUrlHelper url)
        {
            var dto = new DTO.Form4Report()
            {
                ID = entity.ID,

                IssuerID = entity.IssuerID,

                ReporterID = entity.ReporterID,

                ReportID = entity.ReportID,

                IsOfficer = entity.IsOfficer,

                IsDirector = entity.IsDirector,

                Is10PctHolder = entity.Is10PctHolder,

                IsOther = entity.IsOther,

                OtherText = entity.OtherText,

                OfficerTitle = entity.OfficerTitle,

                Date = entity.Date,

                DateSubmitted = entity.DateSubmitted,


            };

            if (url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetForm4Report", "form4reports", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteForm4Report", "form4reports", new { id = dto.ID }), "delete_form4report", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertForm4Report", "form4reports"), "insert_form4report", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateForm4Report", "form4reports"), "update_form4report", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Form4Report Convert(DTO.Form4Report dto)
        {
            var entity = new Interfaces.Entities.Form4Report()
            {

                ID = dto.ID,

                IssuerID = dto.IssuerID,

                ReporterID = dto.ReporterID,

                ReportID = dto.ReportID,

                IsOfficer = dto.IsOfficer,

                IsDirector = dto.IsDirector,

                Is10PctHolder = dto.Is10PctHolder,

                IsOther = dto.IsOther,

                OtherText = dto.OtherText,

                OfficerTitle = dto.OfficerTitle,

                Date = dto.Date,

                DateSubmitted = dto.DateSubmitted,
            };

            return entity;
        }

        public static ITM.Interfaces.Entities.Form4Report Form4ReportFromRow(DataRow row)
        {
            var entity = new ITM.Interfaces.Entities.Form4Report();

            entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
            entity.IssuerID = !DBNull.Value.Equals(row["IssuerID"]) ? (System.Int64)row["IssuerID"] : default(System.Int64);
            entity.ReporterID = !DBNull.Value.Equals(row["ReporterID"]) ? (System.Int64)row["ReporterID"] : default(System.Int64);
            entity.ReportID = !DBNull.Value.Equals(row["ReportID"]) ? (System.String)row["ReportID"] : default(System.String);
            entity.IsOfficer = !DBNull.Value.Equals(row["IsOfficer"]) ? (System.Boolean)row["IsOfficer"] : default(System.Boolean);
            entity.IsDirector = !DBNull.Value.Equals(row["IsDirector"]) ? (System.Boolean)row["IsDirector"] : default(System.Boolean);
            entity.Is10PctHolder = !DBNull.Value.Equals(row["Is10PctHolder"]) ? (System.Boolean)row["Is10PctHolder"] : default(System.Boolean);
            entity.IsOther = !DBNull.Value.Equals(row["IsOther"]) ? (System.Boolean)row["IsOther"] : default(System.Boolean);
            entity.OtherText = !DBNull.Value.Equals(row["OtherText"]) ? (System.String)row["OtherText"] : default(System.String);
            entity.OfficerTitle = !DBNull.Value.Equals(row["OfficerTitle"]) ? (System.String)row["OfficerTitle"] : default(System.String);
            entity.Date = !DBNull.Value.Equals(row["Date"]) ? (System.DateTime)row["Date"] : default(System.DateTime);
            entity.DateSubmitted = !DBNull.Value.Equals(row["DateSubmitted"]) ? (System.DateTime)row["DateSubmitted"] : default(System.DateTime);

            return entity;
        }
    }
}
