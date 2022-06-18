


using ITM.DTO;
using ITM.Utils.Convertors;
using Test.E2E.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net;
using Xunit;

namespace Test.E2E.API.Controllers.V1
{
    public class TestUsersController : E2ETestBase, IClassFixture<WebApplicationFactory<ITM.API.Startup>>
    {
        public TestUsersController(WebApplicationFactory<ITM.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void User_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/users");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<User> dtos = ExtractContentJson<List<User>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void User_Get_Success()
        {
            ITM.Interfaces.Entities.User testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/users/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    User dto = ExtractContentJson<User>(respGet.Result.Content);

                    Assert.NotNull(dto);
                    Assert.NotNull(dto.Links);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void User_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/users/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void User_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/users/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void User_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/users/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void User_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.User testEntity = CreateTestEntity();
                ITM.Interfaces.Entities.User respEntity = null;
                try
                {
                    var reqDto = UserConvertor.Convert(testEntity, null);
                    reqDto.Password = Guid.NewGuid().ToString();

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Login, respDto.Login);
                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                    Assert.Equal(reqDto.MiddleName, respDto.MiddleName);
                    Assert.Equal(reqDto.LastName, respDto.LastName);
                    Assert.Equal(reqDto.FriendlyName, respDto.FriendlyName);

                    respEntity = UserConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void User_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.User testEntity = AddTestEntity();
                try
                {
                    testEntity.Login = "Login a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.PwdHash = "PwdHash a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.Salt = "Salt a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.FirstName = "FirstName a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.MiddleName = "MiddleName a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.LastName = "LastName a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.FriendlyName = "FriendlyName a7505b10bc1f4f44a2d11d831f9a08dd";

                    var reqDto = UserConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Login, respDto.Login);
                    Assert.Equal(reqDto.Salt, respDto.Salt);
                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                    Assert.Equal(reqDto.MiddleName, respDto.MiddleName);
                    Assert.Equal(reqDto.LastName, respDto.LastName);
                    Assert.Equal(reqDto.FriendlyName, respDto.FriendlyName);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void User_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.User testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.Login = "Login a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.PwdHash = "PwdHash a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.Salt = "Salt a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.FirstName = "FirstName a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.MiddleName = "MiddleName a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.LastName = "LastName a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.FriendlyName = "FriendlyName a7505b10bc1f4f44a2d11d831f9a08dd";
                    testEntity.CreatedDate = DateTime.Parse("10/2/2021 10:19:17 PM");
                    testEntity.ModifiedDate = DateTime.Parse("10/2/2021 10:19:17 PM");
                    testEntity.ModifiedByID = 100004;

                    var reqDto = UserConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(ITM.Interfaces.Entities.User entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();



                return dal.Delete(entity.ID
                );
            }
            else
            {
                return false;
            }
        }

        protected ITM.Interfaces.Entities.User CreateTestEntity()
        {
            var entity = new ITM.Interfaces.Entities.User();
            entity.Login = "Login c01c84e70f534803a892bae32ec47695";
            entity.PwdHash = "PwdHash c01c84e70f534803a892bae32ec47695";
            entity.Salt = "Salt c01c84e70f534803a892bae32ec47695";
            entity.FirstName = "FirstName c01c84e70f534803a892bae32ec47695";
            entity.MiddleName = "MiddleName c01c84e70f534803a892bae32ec47695";
            entity.LastName = "LastName c01c84e70f534803a892bae32ec47695";
            entity.FriendlyName = "FriendlyName c01c84e70f534803a892bae32ec47695";
            entity.CreatedDate = DateTime.Parse("10/2/2021 10:19:17 PM");
            entity.ModifiedDate = DateTime.Parse("10/2/2021 10:19:17 PM");
            entity.ModifiedByID = 100005;

            return entity;
        }

        protected ITM.Interfaces.Entities.User AddTestEntity()
        {
            ITM.Interfaces.Entities.User result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private ITM.Interfaces.IUserDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            ITM.Interfaces.IUserDal dal = new ITM.DAL.MSSQL.UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
