
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
    public class TestDerivativeTransactionsController : E2ETestBase
    {
        public TestDerivativeTransactionsController(WebApplicationFactory<ITM.API.Startup> factory) : base(new TestServer(new WebHostBuilder()
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
        public void DerivativeTransaction_GetAll_Success()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/derivativetransactions");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<DerivativeTransaction> dtos = ExtractContentJson<List<DerivativeTransaction>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void DerivativeTransaction_Get_Success()
        {
            ITM.Interfaces.Entities.DerivativeTransaction testEntity = AddTestEntity();
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/derivativetransactions/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    DerivativeTransaction dto = ExtractContentJson<DerivativeTransaction>(respGet.Result.Content);

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
        public void DerivativeTransaction_Get_InvalidID()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/derivativetransactions/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void DerivativeTransaction_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/derivativetransactions/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void DerivativeTransaction_Delete_InvalidID()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/derivativetransactions/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void DerivativeTransaction_Insert_Success()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.DerivativeTransaction testEntity = CreateTestEntity();
                ITM.Interfaces.Entities.DerivativeTransaction respEntity = null;
                try
                {
                    var reqDto = DerivativeTransactionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/derivativetransactions/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    DerivativeTransaction respDto = ExtractContentJson<DerivativeTransaction>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Form4ReportID, respDto.Form4ReportID);
                    Assert.Equal(reqDto.TitleOfDerivative, respDto.TitleOfDerivative);
                    Assert.Equal(reqDto.ConversionExercisePrice, respDto.ConversionExercisePrice);
                    Assert.Equal(reqDto.TransactionDate, respDto.TransactionDate);
                    Assert.Equal(reqDto.TransactionCodeID, respDto.TransactionCodeID);
                    Assert.Equal(reqDto.EarlyVoluntarilyReport, respDto.EarlyVoluntarilyReport);
                    Assert.Equal(reqDto.SharesAmount, respDto.SharesAmount);
                    Assert.Equal(reqDto.DerivativeSecurityPrice, respDto.DerivativeSecurityPrice);
                    Assert.Equal(reqDto.TransactionTypeID, respDto.TransactionTypeID);
                    Assert.Equal(reqDto.DateExercisable, respDto.DateExercisable);
                    Assert.Equal(reqDto.ExpirationDate, respDto.ExpirationDate);
                    Assert.Equal(reqDto.UnderlyingTitle, respDto.UnderlyingTitle);
                    Assert.Equal(reqDto.UnderlyingSharesAmount, respDto.UnderlyingSharesAmount);
                    Assert.Equal(reqDto.AmountFollowingReport, respDto.AmountFollowingReport);
                    Assert.Equal(reqDto.OwnershipTypeID, respDto.OwnershipTypeID);
                    Assert.Equal(reqDto.NatureOfIndirectOwnership, respDto.NatureOfIndirectOwnership);

                    respEntity = DerivativeTransactionConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void DerivativeTransaction_Update_Success()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.DerivativeTransaction testEntity = AddTestEntity();
                try
                {
                    testEntity.Form4ReportID = 100030;
                    testEntity.TitleOfDerivative = "TitleOfDerivative 18b8ac72f4004d5e926503cc48675605";
                    testEntity.ConversionExercisePrice = 877461.587487M;
                    testEntity.TransactionDate = DateTime.Parse("7/11/2024");
                    testEntity.TransactionCodeID = 15;
                    testEntity.EarlyVoluntarilyReport = true;
                    testEntity.SharesAmount = 877461;
                    testEntity.DerivativeSecurityPrice = 877461.587487M;
                    testEntity.TransactionTypeID = 2;
                    testEntity.DateExercisable = DateTime.Parse("11/29/2021");
                    testEntity.ExpirationDate = DateTime.Parse("11/29/2021");
                    testEntity.UnderlyingTitle = "UnderlyingTitle 18b8ac72f4004d5e926503cc48675605";
                    testEntity.UnderlyingSharesAmount = 399887;
                    testEntity.AmountFollowingReport = 399887;
                    testEntity.OwnershipTypeID = 2;
                    testEntity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership 18b8ac72f4004d5e926503cc48675605";

                    var reqDto = DerivativeTransactionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/derivativetransactions/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    DerivativeTransaction respDto = ExtractContentJson<DerivativeTransaction>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Form4ReportID, respDto.Form4ReportID);
                    Assert.Equal(reqDto.TitleOfDerivative, respDto.TitleOfDerivative);
                    Assert.Equal(reqDto.ConversionExercisePrice, respDto.ConversionExercisePrice);
                    Assert.Equal(reqDto.TransactionDate, respDto.TransactionDate);
                    Assert.Equal(reqDto.TransactionCodeID, respDto.TransactionCodeID);
                    Assert.Equal(reqDto.EarlyVoluntarilyReport, respDto.EarlyVoluntarilyReport);
                    Assert.Equal(reqDto.SharesAmount, respDto.SharesAmount);
                    Assert.Equal(reqDto.DerivativeSecurityPrice, respDto.DerivativeSecurityPrice);
                    Assert.Equal(reqDto.TransactionTypeID, respDto.TransactionTypeID);
                    Assert.Equal(reqDto.DateExercisable, respDto.DateExercisable);
                    Assert.Equal(reqDto.ExpirationDate, respDto.ExpirationDate);
                    Assert.Equal(reqDto.UnderlyingTitle, respDto.UnderlyingTitle);
                    Assert.Equal(reqDto.UnderlyingSharesAmount, respDto.UnderlyingSharesAmount);
                    Assert.Equal(reqDto.AmountFollowingReport, respDto.AmountFollowingReport);
                    Assert.Equal(reqDto.OwnershipTypeID, respDto.OwnershipTypeID);
                    Assert.Equal(reqDto.NatureOfIndirectOwnership, respDto.NatureOfIndirectOwnership);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void DerivativeTransaction_Update_InvalidID()
        {
            using (var client = _testServer.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.DerivativeTransaction testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.Form4ReportID = 100030;
                    testEntity.TitleOfDerivative = "TitleOfDerivative 18b8ac72f4004d5e926503cc48675605";
                    testEntity.ConversionExercisePrice = 877461.587487M;
                    testEntity.TransactionDate = DateTime.Parse("7/11/2024");
                    testEntity.TransactionCodeID = 15;
                    testEntity.EarlyVoluntarilyReport = true;
                    testEntity.SharesAmount = 877461;
                    testEntity.DerivativeSecurityPrice = 877461.587487M;
                    testEntity.TransactionTypeID = 2;
                    testEntity.DateExercisable = DateTime.Parse("11/29/2021");
                    testEntity.ExpirationDate = DateTime.Parse("11/29/2021");
                    testEntity.UnderlyingTitle = "UnderlyingTitle 18b8ac72f4004d5e926503cc48675605";
                    testEntity.UnderlyingSharesAmount = 399887;
                    testEntity.AmountFollowingReport = 399887;
                    testEntity.OwnershipTypeID = 2;
                    testEntity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership 18b8ac72f4004d5e926503cc48675605";

                    var reqDto = DerivativeTransactionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/derivativetransactions/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(ITM.Interfaces.Entities.DerivativeTransaction entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(entity.ID);
            }
            else
            {
                return false;
            }
        }

        protected ITM.Interfaces.Entities.DerivativeTransaction CreateTestEntity()
        {
            var entity = new ITM.Interfaces.Entities.DerivativeTransaction();
            entity.Form4ReportID = 100024;
            entity.TitleOfDerivative = "TitleOfDerivative e47440e5380f41bd8905e036b0d96d9f";
            entity.ConversionExercisePrice = 355036.273298M;
            entity.TransactionDate = DateTime.Parse("8/31/2021");
            entity.TransactionCodeID = 16;
            entity.EarlyVoluntarilyReport = false;
            entity.SharesAmount = 355036;
            entity.DerivativeSecurityPrice = 355036.273298M;
            entity.TransactionTypeID = 1;
            entity.DateExercisable = DateTime.Parse("7/11/2024");
            entity.ExpirationDate = DateTime.Parse("7/11/2024");
            entity.UnderlyingTitle = "UnderlyingTitle e47440e5380f41bd8905e036b0d96d9f";
            entity.UnderlyingSharesAmount = 877461;
            entity.AmountFollowingReport = 877461;
            entity.OwnershipTypeID = 1;
            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership e47440e5380f41bd8905e036b0d96d9f";

            return entity;
        }

        protected ITM.Interfaces.Entities.DerivativeTransaction AddTestEntity()
        {
            ITM.Interfaces.Entities.DerivativeTransaction result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private ITM.Interfaces.IDerivativeTransactionDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            ITM.Interfaces.IDerivativeTransactionDal dal = new ITM.DAL.MSSQL.DerivativeTransactionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
