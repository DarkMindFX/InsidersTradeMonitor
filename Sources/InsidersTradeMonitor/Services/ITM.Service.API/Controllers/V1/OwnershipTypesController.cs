

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
    public class OwnershipTypesController : BaseController
    {
        private readonly IOwnershipTypeDal _dalOwnershipType;
        private readonly ILogger<OwnershipTypesController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public OwnershipTypesController( IOwnershipTypeDal dalOwnershipType,
                                    ILogger<OwnershipTypesController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalOwnershipType = dalOwnershipType; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalOwnershipType.GetAll();

            IList<DTO.OwnershipType> dtos = new List<DTO.OwnershipType>();

            foreach (var p in entities)
            {
                var dto = OwnershipTypeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetOwnershipType")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalOwnershipType.Get(id);
            if (entity != null)
            {
                var dto = OwnershipTypeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"OwnershipType was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteOwnershipType")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalOwnershipType.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalOwnershipType.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete OwnershipType [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"OwnershipType not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertOwnershipType")]
        public IActionResult Insert(DTO.OwnershipType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = OwnershipTypeConvertor.Convert(dto);           

            
            OwnershipType newEntity = _dalOwnershipType.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, OwnershipTypeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateOwnershipType")]
        public IActionResult Update(DTO.OwnershipType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = OwnershipTypeConvertor.Convert(dto);

            var existingEntity = _dalOwnershipType.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    OwnershipType entity = _dalOwnershipType.Update(newEntity);

                response = Ok(OwnershipTypeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"OwnershipType not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

