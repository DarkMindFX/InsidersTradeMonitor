

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
    public class ImportRunStatesController : BaseController
    {
        private readonly IImportRunStateDal _dalImportRunState;
        private readonly ILogger<ImportRunStatesController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public ImportRunStatesController( IImportRunStateDal dalImportRunState,
                                    ILogger<ImportRunStatesController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalImportRunState = dalImportRunState; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalImportRunState.GetAll();

            IList<DTO.ImportRunState> dtos = new List<DTO.ImportRunState>();

            foreach (var p in entities)
            {
                var dto = ImportRunStateConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetImportRunState")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalImportRunState.Get(id);
            if (entity != null)
            {
                var dto = ImportRunStateConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ImportRunState was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteImportRunState")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalImportRunState.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalImportRunState.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ImportRunState [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"ImportRunState not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertImportRunState")]
        public IActionResult Insert(DTO.ImportRunState dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ImportRunStateConvertor.Convert(dto);           

            
            ImportRunState newEntity = _dalImportRunState.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, ImportRunStateConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateImportRunState")]
        public IActionResult Update(DTO.ImportRunState dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ImportRunStateConvertor.Convert(dto);

            var existingEntity = _dalImportRunState.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    ImportRunState entity = _dalImportRunState.Update(newEntity);

                response = Ok(ImportRunStateConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ImportRunState not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

