


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
    public class TestTransactionTypesController : E2ETestBase, IClassFixture<WebApplicationFactory<ITM.API.Startup>>
    {
        public TestTransactionTypesController(WebApplicationFactory<ITM.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void TransactionType_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/transactiontypes");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<TransactionType> dtos = ExtractContentJson<List<TransactionType>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void TransactionType_Get_Success()
        {
            ITM.Interfaces.Entities.TransactionType testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/transactiontypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    TransactionType dto = ExtractContentJson<TransactionType>(respGet.Result.Content);

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
        public void TransactionType_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/transactiontypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void TransactionType_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/transactiontypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void TransactionType_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/transactiontypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void TransactionType_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.TransactionType testEntity = CreateTestEntity();
                ITM.Interfaces.Entities.TransactionType respEntity = null;
                try
                {
                    var reqDto = TransactionTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/transactiontypes/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    TransactionType respDto = ExtractContentJson<TransactionType>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Code, respDto.Code);
                                    Assert.Equal(reqDto.Description, respDto.Description);
                
                    respEntity = TransactionTypeConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void TransactionType_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.TransactionType testEntity = AddTestEntity();
                try
                {
                          testEntity.Code = "Code 59c04";
                            testEntity.Description = "Description 59c04fb74a694108a632f81af473d915";
              
                    var reqDto = TransactionTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/transactiontypes/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    TransactionType respDto = ExtractContentJson<TransactionType>(respUpdate.Result.Content);

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
        public void TransactionType_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.TransactionType testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Code = "Code 59c04";
                            testEntity.Description = "Description 59c04fb74a694108a632f81af473d915";
              
                    var reqDto = TransactionTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/transactiontypes/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(ITM.Interfaces.Entities.TransactionType entity)
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

        protected ITM.Interfaces.Entities.TransactionType CreateTestEntity()
        {
            var entity = new ITM.Interfaces.Entities.TransactionType();
                          entity.Code = "Code 9339c";
                            entity.Description = "Description 9339c5655a134603b5c40e8f257d83bb";
              
            return entity;
        }

        protected ITM.Interfaces.Entities.TransactionType AddTestEntity()
        {
            ITM.Interfaces.Entities.TransactionType result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private ITM.Interfaces.ITransactionTypeDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            ITM.Interfaces.ITransactionTypeDal dal = new ITM.DAL.MSSQL.TransactionTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
