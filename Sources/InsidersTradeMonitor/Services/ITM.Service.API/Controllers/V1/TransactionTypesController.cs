

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
    public class TransactionTypesController : BaseController
    {
        private readonly ITransactionTypeDal _dalTransactionType;
        private readonly ILogger<TransactionTypesController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public TransactionTypesController( ITransactionTypeDal dalTransactionType,
                                    ILogger<TransactionTypesController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalTransactionType = dalTransactionType; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalTransactionType.GetAll();

            IList<DTO.TransactionType> dtos = new List<DTO.TransactionType>();

            foreach (var p in entities)
            {
                var dto = TransactionTypeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetTransactionType")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalTransactionType.Get(id);
            if (entity != null)
            {
                var dto = TransactionTypeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"TransactionType was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteTransactionType")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalTransactionType.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalTransactionType.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete TransactionType [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"TransactionType not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertTransactionType")]
        public IActionResult Insert(DTO.TransactionType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = TransactionTypeConvertor.Convert(dto);           

            
            TransactionType newEntity = _dalTransactionType.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, TransactionTypeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateTransactionType")]
        public IActionResult Update(DTO.TransactionType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = TransactionTypeConvertor.Convert(dto);

            var existingEntity = _dalTransactionType.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    TransactionType entity = _dalTransactionType.Update(newEntity);

                response = Ok(TransactionTypeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"TransactionType not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

