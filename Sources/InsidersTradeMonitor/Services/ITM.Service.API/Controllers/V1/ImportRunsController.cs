

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
    public class ImportRunsController : BaseController
    {
        private readonly IImportRunDal _dalImportRun;
        private readonly ILogger<ImportRunsController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public ImportRunsController( IImportRunDal dalImportRun,
                                    ILogger<ImportRunsController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalImportRun = dalImportRun; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImportRun.GetAll();

            IList<DTO.ImportRun> dtos = new List<DTO.ImportRun>();

            foreach (var p in entities)
            {
                var dto = ImportRunConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetImportRun")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalImportRun.Get(id);
            if (entity != null)
            {
                var dto = ImportRunConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ImportRun was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("bystateid/{stateid}")]
        public IActionResult GetByStateID(System.Int64 stateid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImportRun.GetByStateID(stateid);

            IList<DTO.ImportRun> dtos = new List<DTO.ImportRun>();

            foreach (var p in entities)
            {
                var dto = ImportRunConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteImportRun")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalImportRun.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalImportRun.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ImportRun [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"ImportRun not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertImportRun")]
        public IActionResult Insert(DTO.ImportRun dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ImportRunConvertor.Convert(dto);           

            
            ImportRun newEntity = _dalImportRun.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, ImportRunConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateImportRun")]
        public IActionResult Update(DTO.ImportRun dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ImportRunConvertor.Convert(dto);

            var existingEntity = _dalImportRun.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    ImportRun entity = _dalImportRun.Update(newEntity);

                response = Ok(ImportRunConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ImportRun not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

