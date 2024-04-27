
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
    public class TestImportRunsController : E2ETestBase
    {
        public TestImportRunsController() : base(new TestServer(new WebHostBuilder()
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
        public void ImportRun_GetAll_Success()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/importruns");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<ImportRun> dtos = ExtractContentJson<List<ImportRun>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void ImportRun_Get_Success()
        {
            ITM.Interfaces.Entities.ImportRun testEntity = AddTestEntity();
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/importruns/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    ImportRun dto = ExtractContentJson<ImportRun>(respGet.Result.Content);

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
        public void ImportRun_Get_InvalidID()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/importruns/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void ImportRun_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/importruns/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ImportRun_Delete_InvalidID()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/importruns/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void ImportRun_Insert_Success()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.ImportRun testEntity = CreateTestEntity();
                ITM.Interfaces.Entities.ImportRun respEntity = null;
                try
                {
                    var reqDto = ImportRunConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/importruns/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    ImportRun respDto = ExtractContentJson<ImportRun>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.TimeStart, respDto.TimeStart);
                                    Assert.Equal(reqDto.TimeEnd, respDto.TimeEnd);
                                    Assert.Equal(reqDto.RequestJson, respDto.RequestJson);
                                    Assert.Equal(reqDto.StateID, respDto.StateID);
                
                    respEntity = ImportRunConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void ImportRun_Update_Success()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.ImportRun testEntity = AddTestEntity();
                try
                {
                          testEntity.TimeStart = DateTime.Parse("3/10/2025 3:41:20 AM");
                            testEntity.TimeEnd = DateTime.Parse("3/10/2025 3:41:20 AM");
                            testEntity.RequestJson = "RequestJson 08ce57931eb5416bbdc5f1861559b965";
                            testEntity.StateID = 2 ;
              
                    var reqDto = ImportRunConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/importruns/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    ImportRun respDto = ExtractContentJson<ImportRun>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.TimeStart, respDto.TimeStart);
                                    Assert.Equal(reqDto.TimeEnd, respDto.TimeEnd);
                                    Assert.Equal(reqDto.RequestJson, respDto.RequestJson);
                                    Assert.Equal(reqDto.StateID, respDto.StateID);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ImportRun_Update_InvalidID()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.ImportRun testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.TimeStart = DateTime.Parse("3/10/2025 3:41:20 AM");
                            testEntity.TimeEnd = DateTime.Parse("3/10/2025 3:41:20 AM");
                            testEntity.RequestJson = "RequestJson 08ce57931eb5416bbdc5f1861559b965";
                            testEntity.StateID = 2;
              
                    var reqDto = ImportRunConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/importruns/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(ITM.Interfaces.Entities.ImportRun entity)
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

        protected ITM.Interfaces.Entities.ImportRun CreateTestEntity()
        {
            var entity = new ITM.Interfaces.Entities.ImportRun();
                          entity.TimeStart = DateTime.Parse("3/10/2025 3:41:20 AM");
                            entity.TimeEnd = DateTime.Parse("3/10/2025 3:41:20 AM");
                            entity.RequestJson = "RequestJson 76e55a82366b42fcb9dc760205938998";
                            entity.StateID = 3;
              
            return entity;
        }

        protected ITM.Interfaces.Entities.ImportRun AddTestEntity()
        {
            ITM.Interfaces.Entities.ImportRun result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private ITM.Interfaces.IImportRunDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            ITM.Interfaces.IImportRunDal dal = new ITM.DAL.MSSQL.ImportRunDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
