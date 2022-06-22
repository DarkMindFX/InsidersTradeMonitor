

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
using ITM.Interfaces;

namespace ITM.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class ImportRunForm4ReportsController : BaseController
    {
        private readonly Services.Dal.IImportRunForm4ReportDal _dalImportRunForm4Report;
        private readonly ILogger<ImportRunForm4ReportsController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public ImportRunForm4ReportsController(Services.Dal.IImportRunForm4ReportDal dalImportRunForm4Report,
                                    ILogger<ImportRunForm4ReportsController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalImportRunForm4Report = dalImportRunForm4Report; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImportRunForm4Report.GetAll();

            IList<DTO.ImportRunForm4Report> dtos = new List<DTO.ImportRunForm4Report>();

            foreach (var p in entities)
            {
                var dto = ImportRunForm4ReportConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetImportRunForm4Report")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalImportRunForm4Report.Get(id);
            if (entity != null)
            {
                var dto = ImportRunForm4ReportConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ImportRunForm4Report was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("byimportrunid/{importrunid}")]
        public IActionResult GetByImportRunID(System.Int64 importrunid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImportRunForm4Report.GetByImportRunID(importrunid);

            IList<DTO.ImportRunForm4Report> dtos = new List<DTO.ImportRunForm4Report>();

            foreach (var p in entities)
            {
                var dto = ImportRunForm4ReportConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("byform4reportid/{form4reportid}")]
        public IActionResult GetByForm4ReportID(System.Int64 form4reportid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImportRunForm4Report.GetByForm4ReportID(form4reportid);

            IList<DTO.ImportRunForm4Report> dtos = new List<DTO.ImportRunForm4Report>();

            foreach (var p in entities)
            {
                var dto = ImportRunForm4ReportConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteImportRunForm4Report")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalImportRunForm4Report.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalImportRunForm4Report.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ImportRunForm4Report [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"ImportRunForm4Report not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertImportRunForm4Report")]
        public IActionResult Insert(DTO.ImportRunForm4Report dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ImportRunForm4ReportConvertor.Convert(dto);           

            
            ImportRunForm4Report newEntity = _dalImportRunForm4Report.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, ImportRunForm4ReportConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateImportRunForm4Report")]
        public IActionResult Update(DTO.ImportRunForm4Report dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ImportRunForm4ReportConvertor.Convert(dto);

            var existingEntity = _dalImportRunForm4Report.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    ImportRunForm4Report entity = _dalImportRunForm4Report.Update(newEntity);

                response = Ok(ImportRunForm4ReportConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ImportRunForm4Report not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

