


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
    public class TestForm4ReportsController : E2ETestBase, IClassFixture<WebApplicationFactory<ITM.API.Startup>>
    {
        public TestForm4ReportsController(WebApplicationFactory<ITM.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Form4Report_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/form4reports");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Form4Report> dtos = ExtractContentJson<List<Form4Report>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Form4Report_Get_Success()
        {
            ITM.Interfaces.Entities.Form4Report testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/form4reports/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Form4Report dto = ExtractContentJson<Form4Report>(respGet.Result.Content);

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
        public void Form4Report_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/form4reports/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Form4Report_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/form4reports/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Form4Report_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/form4reports/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Form4Report_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.Form4Report testEntity = CreateTestEntity();
                ITM.Interfaces.Entities.Form4Report respEntity = null;
                try
                {
                    var reqDto = Form4ReportConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/form4reports/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Form4Report respDto = ExtractContentJson<Form4Report>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.IssuerID, respDto.IssuerID);
                    Assert.Equal(reqDto.ReporterID, respDto.ReporterID);
                    Assert.Equal(reqDto.IsOfficer, respDto.IsOfficer);
                    Assert.Equal(reqDto.IsDirector, respDto.IsDirector);
                    Assert.Equal(reqDto.Is10PctHolder, respDto.Is10PctHolder);
                    Assert.Equal(reqDto.IsOther, respDto.IsOther);
                    Assert.Equal(reqDto.OtherText, respDto.OtherText);
                    Assert.Equal(reqDto.OfficerTitle, respDto.OfficerTitle);
                    Assert.Equal(reqDto.Date, respDto.Date);

                    respEntity = Form4ReportConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Form4Report_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.Form4Report testEntity = AddTestEntity();
                try
                {
                    testEntity.IssuerID = 100009;
                    testEntity.ReporterID = 100001;
                    testEntity.IsOfficer = true;
                    testEntity.IsDirector = true;
                    testEntity.Is10PctHolder = true;
                    testEntity.IsOther = true;
                    testEntity.OtherText = "OtherText 9e64f6dfb4c54160a770ff3aa0dbb0d0";
                    testEntity.OfficerTitle = "OfficerTitle 9e64f6dfb4c54160a770ff3aa0dbb0d0";
                    testEntity.Date = DateTime.Parse("8/19/2023");

                    var reqDto = Form4ReportConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/form4reports/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Form4Report respDto = ExtractContentJson<Form4Report>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.IssuerID, respDto.IssuerID);
                    Assert.Equal(reqDto.ReporterID, respDto.ReporterID);
                    Assert.Equal(reqDto.IsOfficer, respDto.IsOfficer);
                    Assert.Equal(reqDto.IsDirector, respDto.IsDirector);
                    Assert.Equal(reqDto.Is10PctHolder, respDto.Is10PctHolder);
                    Assert.Equal(reqDto.IsOther, respDto.IsOther);
                    Assert.Equal(reqDto.OtherText, respDto.OtherText);
                    Assert.Equal(reqDto.OfficerTitle, respDto.OfficerTitle);
                    Assert.Equal(reqDto.Date, respDto.Date);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Form4Report_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.Form4Report testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.IssuerID = 100009;
                    testEntity.ReporterID = 100001;
                    testEntity.IsOfficer = true;
                    testEntity.IsDirector = true;
                    testEntity.Is10PctHolder = true;
                    testEntity.IsOther = true;
                    testEntity.OtherText = "OtherText 9e64f6dfb4c54160a770ff3aa0dbb0d0";
                    testEntity.OfficerTitle = "OfficerTitle 9e64f6dfb4c54160a770ff3aa0dbb0d0";
                    testEntity.Date = DateTime.Parse("8/19/2023");

                    var reqDto = Form4ReportConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/form4reports/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(ITM.Interfaces.Entities.Form4Report entity)
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

        protected ITM.Interfaces.Entities.Form4Report CreateTestEntity()
        {
            var entity = new ITM.Interfaces.Entities.Form4Report();
            entity.IssuerID = 100011;
            entity.ReporterID = 100009;
            entity.IsOfficer = true;
            entity.IsDirector = true;
            entity.Is10PctHolder = true;
            entity.IsOther = true;
            entity.OtherText = "OtherText 10191aaab64e4de3bd103718a32d54cf";
            entity.OfficerTitle = "OfficerTitle 10191aaab64e4de3bd103718a32d54cf";
            entity.Date = DateTime.Parse("8/19/2023");

            return entity;
        }

        protected ITM.Interfaces.Entities.Form4Report AddTestEntity()
        {
            ITM.Interfaces.Entities.Form4Report result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private ITM.Interfaces.IForm4ReportDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            ITM.Interfaces.IForm4ReportDal dal = new ITM.DAL.MSSQL.Form4ReportDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
