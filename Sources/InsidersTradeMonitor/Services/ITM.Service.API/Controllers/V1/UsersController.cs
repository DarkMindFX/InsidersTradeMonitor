

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
using ITM.Services.Common.Helpers;

namespace ITM.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class UsersController : BaseController
    {
        private readonly IUserDal _dalUser;
        private readonly ILogger<UsersController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public UsersController(IUserDal dalUser,
                                    ILogger<UsersController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalUser = dalUser;
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUser.GetAll();

            IList<DTO.User> dtos = new List<DTO.User>();

            foreach (var p in entities)
            {
                var dto = UserConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetUser")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalUser.Get(id);
            if (entity != null)
            {
                var dto = UserConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"User was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("bymodifiedbyid/{modifiedbyid}")]
        public IActionResult GetByModifiedByID(System.Int64? modifiedbyid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalUser.GetByModifiedByID(modifiedbyid);

            IList<DTO.User> dtos = new List<DTO.User>();

            foreach (var p in entities)
            {
                var dto = UserConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteUser")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUser.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalUser.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete User [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"User not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertUser")]
        public IActionResult Insert(DTO.User dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = UserConvertor.Convert(dto);
            entity.Salt = PasswordHelper.GenerateSalt(12);
            entity.PwdHash = PasswordHelper.GenerateHash(dto.Password, entity.Salt);

            base.SetCreatedModifiedProperties(entity,
                        "CreatedDate",
                        null);

            User newEntity = _dalUser.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, UserConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateUser")]
        public IActionResult Update(DTO.User dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = UserConvertor.Convert(dto);

            var existingEntity = _dalUser.Get(newEntity.ID);

            if (existingEntity != null)
            {
                newEntity.CreatedDate = existingEntity.CreatedDate;

                base.SetCreatedModifiedProperties(newEntity,
                                        "ModifiedDate",
                                        "ModifiedByID");
                User entity = _dalUser.Update(newEntity);

                response = Ok(UserConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"User not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

