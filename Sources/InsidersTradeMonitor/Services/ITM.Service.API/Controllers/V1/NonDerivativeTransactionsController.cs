

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
    public class NonDerivativeTransactionsController : BaseController
    {
        private readonly INonDerivativeTransactionDal _dalNonDerivativeTransaction;
        private readonly ILogger<NonDerivativeTransactionsController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public NonDerivativeTransactionsController( INonDerivativeTransactionDal dalNonDerivativeTransaction,
                                    ILogger<NonDerivativeTransactionsController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalNonDerivativeTransaction = dalNonDerivativeTransaction; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalNonDerivativeTransaction.GetAll();

            IList<DTO.NonDerivativeTransaction> dtos = new List<DTO.NonDerivativeTransaction>();

            foreach (var p in entities)
            {
                var dto = NonDerivativeTransactionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetNonDerivativeTransaction")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalNonDerivativeTransaction.Get(id);
            if (entity != null)
            {
                var dto = NonDerivativeTransactionConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"NonDerivativeTransaction was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("byform4reportid/{form4reportid}")]
        public IActionResult GetByForm4ReportID(System.Int64 form4reportid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalNonDerivativeTransaction.GetByForm4ReportID(form4reportid);

            IList<DTO.NonDerivativeTransaction> dtos = new List<DTO.NonDerivativeTransaction>();

            foreach (var p in entities)
            {
                var dto = NonDerivativeTransactionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("bytransactioncodeid/{transactioncodeid}")]
        public IActionResult GetByTransactionCodeID(System.Int64 transactioncodeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalNonDerivativeTransaction.GetByTransactionCodeID(transactioncodeid);

            IList<DTO.NonDerivativeTransaction> dtos = new List<DTO.NonDerivativeTransaction>();

            foreach (var p in entities)
            {
                var dto = NonDerivativeTransactionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("bytransactiontypeid/{transactiontypeid}")]
        public IActionResult GetByTransactionTypeID(System.Int64 transactiontypeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalNonDerivativeTransaction.GetByTransactionTypeID(transactiontypeid);

            IList<DTO.NonDerivativeTransaction> dtos = new List<DTO.NonDerivativeTransaction>();

            foreach (var p in entities)
            {
                var dto = NonDerivativeTransactionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("byownershiptypeid/{ownershiptypeid}")]
        public IActionResult GetByOwnershipTypeID(System.Int64 ownershiptypeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalNonDerivativeTransaction.GetByOwnershipTypeID(ownershiptypeid);

            IList<DTO.NonDerivativeTransaction> dtos = new List<DTO.NonDerivativeTransaction>();

            foreach (var p in entities)
            {
                var dto = NonDerivativeTransactionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteNonDerivativeTransaction")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalNonDerivativeTransaction.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalNonDerivativeTransaction.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete NonDerivativeTransaction [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"NonDerivativeTransaction not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertNonDerivativeTransaction")]
        public IActionResult Insert(DTO.NonDerivativeTransaction dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = NonDerivativeTransactionConvertor.Convert(dto);           

            
            NonDerivativeTransaction newEntity = _dalNonDerivativeTransaction.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, NonDerivativeTransactionConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateNonDerivativeTransaction")]
        public IActionResult Update(DTO.NonDerivativeTransaction dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = NonDerivativeTransactionConvertor.Convert(dto);

            var existingEntity = _dalNonDerivativeTransaction.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    NonDerivativeTransaction entity = _dalNonDerivativeTransaction.Update(newEntity);

                response = Ok(NonDerivativeTransactionConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"NonDerivativeTransaction not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

