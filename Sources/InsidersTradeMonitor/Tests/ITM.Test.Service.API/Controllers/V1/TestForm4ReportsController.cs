
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
    public class TestForm4ReportsController : E2ETestBase
    {
        public TestForm4ReportsController() : base(new TestServer(new WebHostBuilder()
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
        public void Form4Report_GetAll_Success()
        {
            using (var client = _testServer.CreateClient())
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
            using (var client = _testServer.CreateClient())
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
            using (var client = _testServer.CreateClient())
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
            using (var client = _testServer.CreateClient())
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
            using (var client = _testServer.CreateClient())
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
            using (var client = _testServer.CreateClient())
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
                    Assert.Equal(reqDto.ReportID, respDto.ReportID);
                    Assert.Equal(reqDto.IsOfficer, respDto.IsOfficer);
                    Assert.Equal(reqDto.IsDirector, respDto.IsDirector);
                    Assert.Equal(reqDto.Is10PctHolder, respDto.Is10PctHolder);
                    Assert.Equal(reqDto.IsOther, respDto.IsOther);
                    Assert.Equal(reqDto.OtherText, respDto.OtherText);
                    Assert.Equal(reqDto.OfficerTitle, respDto.OfficerTitle);
                    Assert.Equal(reqDto.Date, respDto.Date);
                    Assert.Equal(reqDto.DateSubmitted, respDto.DateSubmitted);

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
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.Form4Report testEntity = AddTestEntity();
                try
                {
                    testEntity.IssuerID = 100003;
                    testEntity.ReporterID = 100009;
                    testEntity.ReportID = "ReportID 1501349ac9ad4de58a54f6b2c0ab6b64";
                    testEntity.IsOfficer = true;
                    testEntity.IsDirector = true;
                    testEntity.Is10PctHolder = true;
                    testEntity.IsOther = true;
                    testEntity.OtherText = "OtherText 1501349ac9ad4de58a54f6b2c0ab6b64";
                    testEntity.OfficerTitle = "OfficerTitle 1501349ac9ad4de58a54f6b2c0ab6b64";
                    testEntity.Date = DateTime.Parse("8/14/2023");
                    testEntity.DateSubmitted = DateTime.Parse("8/14/2023");

                    var reqDto = Form4ReportConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/form4reports/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Form4Report respDto = ExtractContentJson<Form4Report>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.IssuerID, respDto.IssuerID);
                    Assert.Equal(reqDto.ReporterID, respDto.ReporterID);
                    Assert.Equal(reqDto.ReportID, respDto.ReportID);
                    Assert.Equal(reqDto.IsOfficer, respDto.IsOfficer);
                    Assert.Equal(reqDto.IsDirector, respDto.IsDirector);
                    Assert.Equal(reqDto.Is10PctHolder, respDto.Is10PctHolder);
                    Assert.Equal(reqDto.IsOther, respDto.IsOther);
                    Assert.Equal(reqDto.OtherText, respDto.OtherText);
                    Assert.Equal(reqDto.OfficerTitle, respDto.OfficerTitle);
                    Assert.Equal(reqDto.Date, respDto.Date);
                    Assert.Equal(reqDto.DateSubmitted, respDto.DateSubmitted);

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
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.Form4Report testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.IssuerID = 100003;
                    testEntity.ReporterID = 100009;
                    testEntity.ReportID = "ReportID 1501349ac9ad4de58a54f6b2c0ab6b64";
                    testEntity.IsOfficer = true;
                    testEntity.IsDirector = true;
                    testEntity.Is10PctHolder = true;
                    testEntity.IsOther = true;
                    testEntity.OtherText = "OtherText 1501349ac9ad4de58a54f6b2c0ab6b64";
                    testEntity.OfficerTitle = "OfficerTitle 1501349ac9ad4de58a54f6b2c0ab6b64";
                    testEntity.Date = DateTime.Parse("8/14/2023");
                    testEntity.DateSubmitted = DateTime.Parse("8/14/2023");

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
            entity.IssuerID = 100004;
            entity.ReporterID = 100009;
            entity.ReportID = "ReportID 0afcb7c826e74b218dc63cad1f413dd9";
            entity.IsOfficer = true;
            entity.IsDirector = true;
            entity.Is10PctHolder = true;
            entity.IsOther = true;
            entity.OtherText = "OtherText 0afcb7c826e74b218dc63cad1f413dd9";
            entity.OfficerTitle = "OfficerTitle 0afcb7c826e74b218dc63cad1f413dd9";
            entity.Date = DateTime.Parse("8/14/2023");
            entity.DateSubmitted = DateTime.Parse("8/14/2023");

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
