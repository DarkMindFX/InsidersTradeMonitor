

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
    public class TransactionCodesController : BaseController
    {
        private readonly ITransactionCodeDal _dalTransactionCode;
        private readonly ILogger<TransactionCodesController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public TransactionCodesController( ITransactionCodeDal dalTransactionCode,
                                    ILogger<TransactionCodesController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalTransactionCode = dalTransactionCode; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalTransactionCode.GetAll();

            IList<DTO.TransactionCode> dtos = new List<DTO.TransactionCode>();

            foreach (var p in entities)
            {
                var dto = TransactionCodeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetTransactionCode")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalTransactionCode.Get(id);
            if (entity != null)
            {
                var dto = TransactionCodeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"TransactionCode was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteTransactionCode")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalTransactionCode.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalTransactionCode.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete TransactionCode [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"TransactionCode not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertTransactionCode")]
        public IActionResult Insert(DTO.TransactionCode dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = TransactionCodeConvertor.Convert(dto);           

            
            TransactionCode newEntity = _dalTransactionCode.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, TransactionCodeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateTransactionCode")]
        public IActionResult Update(DTO.TransactionCode dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = TransactionCodeConvertor.Convert(dto);

            var existingEntity = _dalTransactionCode.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    TransactionCode entity = _dalTransactionCode.Update(newEntity);

                response = Ok(TransactionCodeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"TransactionCode not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

