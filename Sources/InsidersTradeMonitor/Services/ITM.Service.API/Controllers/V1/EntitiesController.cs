

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
    public class EntitiesController : BaseController
    {
        private readonly IEntityDal _dalEntity;
        private readonly ILogger<EntitiesController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public EntitiesController(IEntityDal dalEntity,
                                    ILogger<EntitiesController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalEntity = dalEntity;
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalEntity.GetAll();

            IList<DTO.Entity> dtos = new List<DTO.Entity>();

            foreach (var p in entities)
            {
                var dto = EntityConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetEntity")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalEntity.Get((long)id);
            if (entity != null)
            {
                var dto = EntityConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Entity was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("byentitytypeid/{entitytypeid}")]
        public IActionResult GetByEntityTypeID(System.Int64 entitytypeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalEntity.GetByEntityTypeID(entitytypeid);

            IList<DTO.Entity> dtos = new List<DTO.Entity>();

            foreach (var p in entities)
            {
                var dto = EntityConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteEntity")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalEntity.Get((long)id);

            if (existingEntity != null)
            {
                bool removed = _dalEntity.Delete((long)id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Entity [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Entity not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertEntity")]
        public IActionResult Insert(DTO.Entity dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = EntityConvertor.Convert(dto);


            Entity newEntity = _dalEntity.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, EntityConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateEntity")]
        public IActionResult Update(DTO.Entity dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = EntityConvertor.Convert(dto);

            var existingEntity = _dalEntity.Get((long)newEntity.ID);

            if (existingEntity != null)
            {
                Entity entity = _dalEntity.Update(newEntity);

                response = Ok(EntityConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Entity not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [HttpGet("monitoredlist")]
        public IActionResult GetMonitoredList()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalEntity.GetMonitoredList();

            IList<DTO.Entity> dtos = new List<DTO.Entity>();

            foreach (var p in entities)
            {
                var dto = EntityConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

