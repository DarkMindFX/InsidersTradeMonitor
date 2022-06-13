

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
    public class DerivativeTransactionsController : BaseController
    {
        private readonly IDerivativeTransactionDal _dalDerivativeTransaction;
        private readonly ILogger<DerivativeTransactionsController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public DerivativeTransactionsController( IDerivativeTransactionDal dalDerivativeTransaction,
                                    ILogger<DerivativeTransactionsController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalDerivativeTransaction = dalDerivativeTransaction; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalDerivativeTransaction.GetAll();

            IList<DTO.DerivativeTransaction> dtos = new List<DTO.DerivativeTransaction>();

            foreach (var p in entities)
            {
                var dto = DerivativeTransactionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetDerivativeTransaction")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalDerivativeTransaction.Get(id);
            if (entity != null)
            {
                var dto = DerivativeTransactionConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"DerivativeTransaction was not found [ids:{id}]");
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

            var entities = _dalDerivativeTransaction.GetByForm4ReportID(form4reportid);

            IList<DTO.DerivativeTransaction> dtos = new List<DTO.DerivativeTransaction>();

            foreach (var p in entities)
            {
                var dto = DerivativeTransactionConvertor.Convert(p, this.Url);

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

            var entities = _dalDerivativeTransaction.GetByTransactionCodeID(transactioncodeid);

            IList<DTO.DerivativeTransaction> dtos = new List<DTO.DerivativeTransaction>();

            foreach (var p in entities)
            {
                var dto = DerivativeTransactionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("bytransactiontypeid/{transactiontypeid}")]
        public IActionResult GetByTransactionTypeID(System.Int64? transactiontypeid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalDerivativeTransaction.GetByTransactionTypeID(transactiontypeid);

            IList<DTO.DerivativeTransaction> dtos = new List<DTO.DerivativeTransaction>();

            foreach (var p in entities)
            {
                var dto = DerivativeTransactionConvertor.Convert(p, this.Url);

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

            var entities = _dalDerivativeTransaction.GetByOwnershipTypeID(ownershiptypeid);

            IList<DTO.DerivativeTransaction> dtos = new List<DTO.DerivativeTransaction>();

            foreach (var p in entities)
            {
                var dto = DerivativeTransactionConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteDerivativeTransaction")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalDerivativeTransaction.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalDerivativeTransaction.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete DerivativeTransaction [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"DerivativeTransaction not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertDerivativeTransaction")]
        public IActionResult Insert(DTO.DerivativeTransaction dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = DerivativeTransactionConvertor.Convert(dto);           

            
            DerivativeTransaction newEntity = _dalDerivativeTransaction.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, DerivativeTransactionConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateDerivativeTransaction")]
        public IActionResult Update(DTO.DerivativeTransaction dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = DerivativeTransactionConvertor.Convert(dto);

            var existingEntity = _dalDerivativeTransaction.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    DerivativeTransaction entity = _dalDerivativeTransaction.Update(newEntity);

                response = Ok(DerivativeTransactionConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"DerivativeTransaction not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

