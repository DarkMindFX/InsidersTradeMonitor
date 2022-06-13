

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ITM.API.Filters;
using ITM.Interfaces.Entities;
using ITM.Utils.Convertors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using ITM.API.Helpers;
using ITM.Services.Dal;


namespace ITM.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class Form4ReportsController : BaseController
    {
        private readonly IForm4ReportDal _dalForm4Report;
        private readonly ILogger<Form4ReportsController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public Form4ReportsController( IForm4ReportDal dalForm4Report,
                                    ILogger<Form4ReportsController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalForm4Report = dalForm4Report; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalForm4Report.GetAll();

            IList<DTO.Form4Report> dtos = new List<DTO.Form4Report>();

            foreach (var p in entities)
            {
                var dto = Form4ReportConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetForm4Report")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalForm4Report.Get(id);
            if (entity != null)
            {
                var dto = Form4ReportConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Form4Report was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("byissuerid/{issuerid}")]
        public IActionResult GetByIssuerID(System.Int64 issuerid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalForm4Report.GetByIssuerID(issuerid);

            IList<DTO.Form4Report> dtos = new List<DTO.Form4Report>();

            foreach (var p in entities)
            {
                var dto = Form4ReportConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("byreporterid/{reporterid}")]
        public IActionResult GetByReporterID(System.Int64 reporterid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalForm4Report.GetByReporterID(reporterid);

            IList<DTO.Form4Report> dtos = new List<DTO.Form4Report>();

            foreach (var p in entities)
            {
                var dto = Form4ReportConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteForm4Report")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalForm4Report.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalForm4Report.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Form4Report [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Form4Report not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertForm4Report")]
        public IActionResult Insert(DTO.Form4Report dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = Form4ReportConvertor.Convert(dto);           

            
            Form4Report newEntity = _dalForm4Report.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, Form4ReportConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateForm4Report")]
        public IActionResult Update(DTO.Form4Report dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = Form4ReportConvertor.Convert(dto);

            var existingEntity = _dalForm4Report.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    Form4Report entity = _dalForm4Report.Update(newEntity);

                response = Ok(Form4ReportConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Form4Report not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

