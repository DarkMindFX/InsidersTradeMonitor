





using Microsoft.AspNetCore.Mvc;
using System;

namespace ITM.Utils.Convertors
{
    public class Form4ReportConvertor
    {
        public static ITM.DTO.Form4Report Convert(ITM.Interfaces.Entities.Form4Report entity, IUrlHelper url)
        {
            var dto = new ITM.DTO.Form4Report()
            {
                ID = entity.ID,

                IssuerID = entity.IssuerID,

                ReporterID = entity.ReporterID,

                IsOfficer = entity.IsOfficer,

                IsDirector = entity.IsDirector,

                Is10PctHolder = entity.Is10PctHolder,

                IsOther = entity.IsOther,

                OtherText = entity.OtherText,

                OfficerTitle = entity.OfficerTitle,

                Date = entity.Date,


            };

            if (url != null)
            {
                dto.Links.Add(new ITM.DTO.Link(url.Action("GetForm4Report", "form4reports", new { id = dto.ID }), "self", "GET"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("DeleteForm4Report", "form4reports", new { id = dto.ID }), "delete_form4report", "DELETE"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("InsertForm4Report", "form4reports"), "insert_form4report", "POST"));
                dto.Links.Add(new ITM.DTO.Link(url.Action("UpdateForm4Report", "form4reports"), "update_form4report", "PUT"));
            }
            return dto;

        }

        public static ITM.Interfaces.Entities.Form4Report Convert(ITM.DTO.Form4Report dto)
        {
            var entity = new ITM.Interfaces.Entities.Form4Report()
            {

                ID = dto.ID,

                IssuerID = dto.IssuerID,

                ReporterID = dto.ReporterID,

                IsOfficer = dto.IsOfficer,

                IsDirector = dto.IsDirector,

                Is10PctHolder = dto.Is10PctHolder,

                IsOther = dto.IsOther,

                OtherText = dto.OtherText,

                OfficerTitle = dto.OfficerTitle,

                Date = dto.Date,
            };

            return entity;
        }
    }
}
