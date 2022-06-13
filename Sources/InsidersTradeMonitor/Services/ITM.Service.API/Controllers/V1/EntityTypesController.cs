

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
    public class EntityTypesController : BaseController
    {
        private readonly IEntityTypeDal _dalEntityType;
        private readonly ILogger<EntityTypesController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public EntityTypesController( IEntityTypeDal dalEntityType,
                                    ILogger<EntityTypesController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalEntityType = dalEntityType; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalEntityType.GetAll();

            IList<DTO.EntityType> dtos = new List<DTO.EntityType>();

            foreach (var p in entities)
            {
                var dto = EntityTypeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetEntityType")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalEntityType.Get(id);
            if (entity != null)
            {
                var dto = EntityTypeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"EntityType was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteEntityType")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalEntityType.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalEntityType.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete EntityType [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"EntityType not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertEntityType")]
        public IActionResult Insert(DTO.EntityType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = EntityTypeConvertor.Convert(dto);           

            
            EntityType newEntity = _dalEntityType.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, EntityTypeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateEntityType")]
        public IActionResult Update(DTO.EntityType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = EntityTypeConvertor.Convert(dto);

            var existingEntity = _dalEntityType.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    EntityType entity = _dalEntityType.Update(newEntity);

                response = Ok(EntityTypeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"EntityType not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

