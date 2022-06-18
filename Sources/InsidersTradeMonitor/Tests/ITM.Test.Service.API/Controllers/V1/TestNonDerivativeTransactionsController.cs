


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
    public class TestNonDerivativeTransactionsController : E2ETestBase, IClassFixture<WebApplicationFactory<ITM.API.Startup>>
    {
        public TestNonDerivativeTransactionsController(WebApplicationFactory<ITM.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void NonDerivativeTransaction_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/nonderivativetransactions");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<NonDerivativeTransaction> dtos = ExtractContentJson<List<NonDerivativeTransaction>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void NonDerivativeTransaction_Get_Success()
        {
            ITM.Interfaces.Entities.NonDerivativeTransaction testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/nonderivativetransactions/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    NonDerivativeTransaction dto = ExtractContentJson<NonDerivativeTransaction>(respGet.Result.Content);

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
        public void NonDerivativeTransaction_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/nonderivativetransactions/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void NonDerivativeTransaction_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/nonderivativetransactions/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void NonDerivativeTransaction_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/nonderivativetransactions/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void NonDerivativeTransaction_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.NonDerivativeTransaction testEntity = CreateTestEntity();
                ITM.Interfaces.Entities.NonDerivativeTransaction respEntity = null;
                try
                {
                    var reqDto = NonDerivativeTransactionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/nonderivativetransactions/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    NonDerivativeTransaction respDto = ExtractContentJson<NonDerivativeTransaction>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Form4ReportID, respDto.Form4ReportID);
                    Assert.Equal(reqDto.TitleOfSecurity, respDto.TitleOfSecurity);
                    Assert.Equal(reqDto.TransactionDate, respDto.TransactionDate);
                    Assert.Equal(reqDto.DeemedExecDate, respDto.DeemedExecDate);
                    Assert.Equal(reqDto.TransactionCodeID, respDto.TransactionCodeID);
                    Assert.Equal(reqDto.EarlyVoluntarilyReport, respDto.EarlyVoluntarilyReport);
                    Assert.Equal(reqDto.SharesAmount, respDto.SharesAmount);
                    Assert.Equal(reqDto.TransactionTypeID, respDto.TransactionTypeID);
                    Assert.Equal(reqDto.Price, respDto.Price);
                    Assert.Equal(reqDto.AmountFollowingReport, respDto.AmountFollowingReport);
                    Assert.Equal(reqDto.OwnershipTypeID, respDto.OwnershipTypeID);
                    Assert.Equal(reqDto.NatureOfIndirectOwnership, respDto.NatureOfIndirectOwnership);

                    respEntity = NonDerivativeTransactionConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void NonDerivativeTransaction_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.NonDerivativeTransaction testEntity = AddTestEntity();
                try
                {
                    testEntity.Form4ReportID = 100014;
                    testEntity.TitleOfSecurity = "TitleOfSecurity 47b081d80d61427caa00c5bf5ba11872";
                    testEntity.TransactionDate = DateTime.Parse("2/16/2024");
                    testEntity.DeemedExecDate = DateTime.Parse("2/16/2024");
                    testEntity.TransactionCodeID = 12;
                    testEntity.EarlyVoluntarilyReport = false;
                    testEntity.SharesAmount = 803542;
                    testEntity.TransactionTypeID = 1;
                    testEntity.Price = 803542.557081M;
                    testEntity.AmountFollowingReport = 803542;
                    testEntity.OwnershipTypeID = 1;
                    testEntity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership 47b081d80d61427caa00c5bf5ba11872";

                    var reqDto = NonDerivativeTransactionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/nonderivativetransactions/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    NonDerivativeTransaction respDto = ExtractContentJson<NonDerivativeTransaction>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Form4ReportID, respDto.Form4ReportID);
                    Assert.Equal(reqDto.TitleOfSecurity, respDto.TitleOfSecurity);
                    Assert.Equal(reqDto.TransactionDate, respDto.TransactionDate);
                    Assert.Equal(reqDto.DeemedExecDate, respDto.DeemedExecDate);
                    Assert.Equal(reqDto.TransactionCodeID, respDto.TransactionCodeID);
                    Assert.Equal(reqDto.EarlyVoluntarilyReport, respDto.EarlyVoluntarilyReport);
                    Assert.Equal(reqDto.SharesAmount, respDto.SharesAmount);
                    Assert.Equal(reqDto.TransactionTypeID, respDto.TransactionTypeID);
                    Assert.Equal(reqDto.Price, respDto.Price);
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
        public void NonDerivativeTransaction_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                ITM.Interfaces.Entities.NonDerivativeTransaction testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.Form4ReportID = 100014;
                    testEntity.TitleOfSecurity = "TitleOfSecurity 47b081d80d61427caa00c5bf5ba11872";
                    testEntity.TransactionDate = DateTime.Parse("2/16/2024");
                    testEntity.DeemedExecDate = DateTime.Parse("2/16/2024");
                    testEntity.TransactionCodeID = 12;
                    testEntity.EarlyVoluntarilyReport = false;
                    testEntity.SharesAmount = 803542;
                    testEntity.TransactionTypeID = 1;
                    testEntity.Price = 803542.557081M;
                    testEntity.AmountFollowingReport = 803542;
                    testEntity.OwnershipTypeID = 1;
                    testEntity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership 47b081d80d61427caa00c5bf5ba11872";

                    var reqDto = NonDerivativeTransactionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/nonderivativetransactions/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(ITM.Interfaces.Entities.NonDerivativeTransaction entity)
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

        protected ITM.Interfaces.Entities.NonDerivativeTransaction CreateTestEntity()
        {
            var entity = new ITM.Interfaces.Entities.NonDerivativeTransaction();
            entity.Form4ReportID = 100023;
            entity.TitleOfSecurity = "TitleOfSecurity a3bbb255cbd948a58e78deb3e9167fed";
            entity.TransactionDate = DateTime.Parse("4/6/2021");
            entity.DeemedExecDate = DateTime.Parse("4/6/2021");
            entity.TransactionCodeID = 19;
            entity.EarlyVoluntarilyReport = false;
            entity.SharesAmount = 281117;
            entity.TransactionTypeID = 2;
            entity.Price = 281117.242892M;
            entity.AmountFollowingReport = 281117;
            entity.OwnershipTypeID = 1;
            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership a3bbb255cbd948a58e78deb3e9167fed";

            return entity;
        }

        protected ITM.Interfaces.Entities.NonDerivativeTransaction AddTestEntity()
        {
            ITM.Interfaces.Entities.NonDerivativeTransaction result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private ITM.Interfaces.INonDerivativeTransactionDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            ITM.Interfaces.INonDerivativeTransactionDal dal = new ITM.DAL.MSSQL.NonDerivativeTransactionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
