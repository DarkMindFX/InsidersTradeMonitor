using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ITM.Interfaces.Entities;
using ITM.Utils.Convertors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ITM.API.Helpers;
using ITM.API.Filters;
using ITM.Services.Common.Helpers;

namespace ITM.API.Controllers.V1
{
    [Route("api/v1/users")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class RpcUserOperationsController : BaseController
    {

        private readonly ITM.Services.Dal.IUserDal _dalUser;
        private readonly ILogger<UsersController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public RpcUserOperationsController(ITM.Services.Dal.IUserDal dalUser,
                                        ILogger<UsersController> logger,
                                        IOptions<AppSettings> appSettings)
        {
            _dalUser = dalUser;
            _logger = logger;
            _appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("login"), ActionName("Login")]
        public IActionResult Login(DTO.LoginRequest dtoLogin)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalUser.GetAll().FirstOrDefault(u => u.Login.ToLower() == dtoLogin.Login.ToLower());
            if (existingEntity != null)
            {
                string pwdHash = PasswordHelper.GenerateHash(dtoLogin.Password, existingEntity.Salt);
                if (pwdHash.Equals(existingEntity.PwdHash))
                {
                    var dtExpires = DateTime.Now.AddSeconds(_appSettings.Value.SessionTimeout);
                    var sToken = JWTHelper.GenerateToken(existingEntity, dtExpires, _appSettings.Value.Secret);

                    var dtoResponse = new DTO.LoginResponse()
                    {
                        User = UserConvertor.Convert(existingEntity, this.Url),
                        Token = sToken,
                        Expires = dtExpires
                    };

                    response = Ok(dtoResponse);
                }
                else
                {
                    response = Forbid();
                }
            }
            else
            {
                response = NotFound($"User not found [login:{dtoLogin.Login}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        [AllowAnonymous]
        [HttpPost("register"), ActionName("Register")]
        public IActionResult Register(DTO.RegisterRequest dtoRegister)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            if (string.IsNullOrEmpty(dtoRegister.User.Password))
            {
                response = StatusCode((int)HttpStatusCode.BadRequest,
                    new DTO.Error() { Message = $"Password is empty" });
            }
            else if (string.IsNullOrEmpty(dtoRegister.User.Login))
            {
                response = StatusCode((int)HttpStatusCode.BadRequest,
                    new DTO.Error() { Message = $"Login is empty" });
            }
            else
            {

                // Inserting new user
                var entityUser = UserConvertor.Convert(dtoRegister.User);
                entityUser.Salt = PasswordHelper.GenerateSalt(12);
                entityUser.PwdHash = PasswordHelper.GenerateHash(dtoRegister.User.Password, entityUser.Salt);

                base.SetCreatedModifiedProperties(entityUser,
                            "CreatedDate",
                            null);

                User newEntityUser = _dalUser.Insert(entityUser);

                // Preparing response
                response = StatusCode((int)HttpStatusCode.Created,
                                        new DTO.RegisterResponse()
                                        {
                                            User = UserConvertor.Convert(newEntityUser, this.Url)
                                        });

                _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");
            }

            return response;
        }

        #region Support methods


        #endregion
    }
}
