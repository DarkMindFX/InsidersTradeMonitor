


using ITM.DTO;
using ITM.Utils.Convertors;
using Test.E2E.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net;
using Xunit;
using System.Linq;

namespace Test.E2E.API.Controllers.V1
{
    public class TestEntitiesController : E2ETestBase, IClassFixture<WebApplicationFactory<ITM.API.Startup>>
    {
        public TestEntitiesController(WebApplicationFactory<ITM.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Entity_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/entities");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Entity> dtos = ExtractContentJson<List<Entity>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Entity_GetMonitoredList_Success()
        {
            ITM.Interfaces.Entities.Entity testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var respGetAll = client.GetAsync($"/api/v1/entities/monitoredlist");

                    Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                    IList<Entity> dtos = ExtractContentJson<List<Entity>>(respGetAll.Result.Content);

                    Assert.NotEmpty(dtos);
                    Assert.NotEmpty(dtos.Where(e => e.IsMonitored));
                    Assert.Empty(dtos.Where(e => !e.IsMonitored));
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Entity_Get_Success()
        {
            ITM.Interfaces.Entities.Entity testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/entities/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Entity dto = ExtractContentJson<Entity>(respGet.Result.Content);

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
        public void Entity_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/entities/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Entity_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/entities/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Entity_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/entities/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Entity_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.Entity testEntity = CreateTestEntity();
                ITM.Interfaces.Entities.Entity respEntity = null;
                try
                {
                    var reqDto = EntityConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/entities/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Entity respDto = ExtractContentJson<Entity>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.EntityTypeID, respDto.EntityTypeID);
                    Assert.Equal(reqDto.CIK, respDto.CIK);
                    Assert.Equal(reqDto.Name, respDto.Name);
                    Assert.Equal(reqDto.TradingSymbol, respDto.TradingSymbol);
                    Assert.Equal(reqDto.IsMonitored, respDto.IsMonitored);

                    respEntity = EntityConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Entity_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.Entity testEntity = AddTestEntity();
                try
                {
                    testEntity.EntityTypeID = 2;
                    testEntity.CIK = 669;
                    testEntity.Name = "Name 904b28b988bd4c48a13ccb777ee94825";
                    testEntity.TradingSymbol = "TradingSymbol 904b28b988bd4c48a13ccb777ee94825";
                    testEntity.IsMonitored = true;

                    var reqDto = EntityConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/entities/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Entity respDto = ExtractContentJson<Entity>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.EntityTypeID, respDto.EntityTypeID);
                    Assert.Equal(reqDto.CIK, respDto.CIK);
                    Assert.Equal(reqDto.Name, respDto.Name);
                    Assert.Equal(reqDto.TradingSymbol, respDto.TradingSymbol);
                    Assert.Equal(reqDto.IsMonitored, respDto.IsMonitored);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Entity_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.Entity testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.EntityTypeID = 2;
                    testEntity.CIK = 669;
                    testEntity.Name = "Name 904b28b988bd4c48a13ccb777ee94825";
                    testEntity.TradingSymbol = "TradingSymbol 904b28b988bd4c48a13ccb777ee94825";
                    testEntity.IsMonitored = true;

                    var reqDto = EntityConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/entities/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(ITM.Interfaces.Entities.Entity entity)
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

        protected ITM.Interfaces.Entities.Entity CreateTestEntity()
        {
            var entity = new ITM.Interfaces.Entities.Entity();
            entity.EntityTypeID = 2;
            entity.CIK = 669;
            entity.Name = "Name 191f2739e3964fa099dbca598c15e616";
            entity.TradingSymbol = "TradingSymbol 191f2739e3964fa099dbca598c15e616";
            entity.IsMonitored = true;

            return entity;
        }

        protected ITM.Interfaces.Entities.Entity AddTestEntity()
        {
            ITM.Interfaces.Entities.Entity result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private ITM.Interfaces.IEntityDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            ITM.Interfaces.IEntityDal dal = new ITM.DAL.MSSQL.EntityDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
