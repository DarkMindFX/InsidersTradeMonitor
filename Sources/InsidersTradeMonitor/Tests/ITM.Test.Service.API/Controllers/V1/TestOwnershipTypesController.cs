using ITM.DTO;
using ITM.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Test.E2E.API.Controllers.V1
{
    public class TestOwnershipTypesController : E2ETestBase
    {
        public TestOwnershipTypesController() : base(new TestServer(new WebHostBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.ServiceAPI.json");
                })
                .UseStartup<ITM.API.Startup>())
            )
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void OwnershipType_GetAll_Success()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/ownershiptypes");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<OwnershipType> dtos = ExtractContentJson<List<OwnershipType>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void OwnershipType_Get_Success()
        {
            ITM.Interfaces.Entities.OwnershipType testEntity = AddTestEntity();
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/ownershiptypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    OwnershipType dto = ExtractContentJson<OwnershipType>(respGet.Result.Content);

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
        public void OwnershipType_Get_InvalidID()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/ownershiptypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void OwnershipType_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/ownershiptypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void OwnershipType_Delete_InvalidID()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/ownershiptypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void OwnershipType_Insert_Success()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.OwnershipType testEntity = CreateTestEntity();
                ITM.Interfaces.Entities.OwnershipType respEntity = null;
                try
                {
                    var reqDto = OwnershipTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/ownershiptypes/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    OwnershipType respDto = ExtractContentJson<OwnershipType>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Code, respDto.Code);
                                    Assert.Equal(reqDto.Description, respDto.Description);
                
                    respEntity = OwnershipTypeConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void OwnershipType_Update_Success()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.OwnershipType testEntity = AddTestEntity();
                try
                {
                          testEntity.Code = "C";
                            testEntity.Description = "Description 29feead753b04a76a2dffc753ebf6d81";
              
                    var reqDto = OwnershipTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/ownershiptypes/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    OwnershipType respDto = ExtractContentJson<OwnershipType>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Code, respDto.Code);
                                    Assert.Equal(reqDto.Description, respDto.Description);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void OwnershipType_Update_InvalidID()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.OwnershipType testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Code = "C";
                            testEntity.Description = "Description 29feead753b04a76a2dffc753ebf6d81";
              
                    var reqDto = OwnershipTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/ownershiptypes/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(ITM.Interfaces.Entities.OwnershipType entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();



                return dal.Delete(                        entity.ID
                );
            }
            else
            {
                return false;
            }
        }

        protected ITM.Interfaces.Entities.OwnershipType CreateTestEntity()
        {
            var entity = new ITM.Interfaces.Entities.OwnershipType();
                          entity.Code = "C";
                            entity.Description = "Description 8bd6bbe9e7b54a28a73c190a3b05040d";
              
            return entity;
        }

        protected ITM.Interfaces.Entities.OwnershipType AddTestEntity()
        {
            ITM.Interfaces.Entities.OwnershipType result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private ITM.Interfaces.IOwnershipTypeDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            ITM.Interfaces.IOwnershipTypeDal dal = new ITM.DAL.MSSQL.OwnershipTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
