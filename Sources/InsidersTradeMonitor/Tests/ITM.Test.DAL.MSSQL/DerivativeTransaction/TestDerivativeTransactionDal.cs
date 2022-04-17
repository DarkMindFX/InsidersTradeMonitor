


using PPT.DAL.MSSQL;
using PPT.Interfaces;
using PPT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Test.PPT.DAL.MSSQL
{
    public class TestDerivativeTransactionDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IDerivativeTransactionDal dal = new DerivativeTransactionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void DerivativeTransaction_GetAll_Success()
        {
            var dal = PrepareDerivativeTransactionDal("DALInitParams");

            IList<DerivativeTransaction> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("DerivativeTransaction\\000.GetDetails.Success")]
        public void DerivativeTransaction_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDerivativeTransactionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            DerivativeTransaction entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("TitleOfDerivative c40605e344bc4b33af0612a39c79085b", entity.TitleOfDerivative);
                            Assert.AreEqual(320975.769926M, entity.ConversionExercisePrice);
                            Assert.AreEqual(DateTime.Parse("4/24/2021 6:37:45 AM"), entity.TransactionDate);
                            Assert.AreEqual(13, entity.TransactionCodeID);
                            Assert.AreEqual(false, entity.EarlyVoluntarilyReport);
                            Assert.AreEqual(888251, entity.SharesAmount);
                            Assert.AreEqual(888251.712494M, entity.DerivativeSecurityPrice);
                            Assert.AreEqual(1, entity.TransactionTypeID);
                            Assert.AreEqual(DateTime.Parse("8/30/2024 12:51:45 PM"), entity.DateExercisable);
                            Assert.AreEqual(DateTime.Parse("8/30/2024 12:51:45 PM"), entity.ExpirationDate);
                            Assert.AreEqual("UnderlyingTitle c40605e344bc4b33af0612a39c79085b", entity.UnderlyingTitle);
                            Assert.AreEqual(933102, entity.UnderlyingSharesAmount);
                            Assert.AreEqual(933102, entity.AmountFollowingReport);
                            Assert.AreEqual("NatureOfIndirectOwnership c40605e344bc4b33af0612a39c79085b", entity.NatureOfIndirectOwnership);
                      }

        [Test]
        public void DerivativeTransaction_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareDerivativeTransactionDal("DALInitParams");

            DerivativeTransaction entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("DerivativeTransaction\\010.Delete.Success")]
        public void DerivativeTransaction_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDerivativeTransactionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void DerivativeTransaction_Delete_InvalidId()
        {
            var dal = PrepareDerivativeTransactionDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("DerivativeTransaction\\020.Insert.Success")]
        public void DerivativeTransaction_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareDerivativeTransactionDal("DALInitParams");

            var entity = new DerivativeTransaction();
                          entity.TitleOfDerivative = "TitleOfDerivative d570b00042d94321889af3c435f3652c";
                            entity.ConversionExercisePrice = 67654.226007M;
                            entity.TransactionDate = DateTime.Parse("12/4/2019 4:52:45 AM");
                            entity.TransactionCodeID = 2;
                            entity.EarlyVoluntarilyReport = true;              
                            entity.SharesAmount = 67655;
                            entity.DerivativeSecurityPrice = 67654.226007M;
                            entity.TransactionTypeID = 1;
                            entity.DateExercisable = DateTime.Parse("12/4/2019 4:52:45 AM");
                            entity.ExpirationDate = DateTime.Parse("12/4/2019 4:52:45 AM");
                            entity.UnderlyingTitle = "UnderlyingTitle d570b00042d94321889af3c435f3652c";
                            entity.UnderlyingSharesAmount = 67655;
                            entity.AmountFollowingReport = 67655;
                            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership d570b00042d94321889af3c435f3652c";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("TitleOfDerivative d570b00042d94321889af3c435f3652c", entity.TitleOfDerivative);
                            Assert.AreEqual(67654.226007M, entity.ConversionExercisePrice);
                            Assert.AreEqual(DateTime.Parse("12/4/2019 4:52:45 AM"), entity.TransactionDate);
                            Assert.AreEqual(2, entity.TransactionCodeID);
                            Assert.AreEqual(true, entity.EarlyVoluntarilyReport);
                            Assert.AreEqual(67655, entity.SharesAmount);
                            Assert.AreEqual(67654.226007M, entity.DerivativeSecurityPrice);
                            Assert.AreEqual(1, entity.TransactionTypeID);
                            Assert.AreEqual(DateTime.Parse("12/4/2019 4:52:45 AM"), entity.DateExercisable);
                            Assert.AreEqual(DateTime.Parse("12/4/2019 4:52:45 AM"), entity.ExpirationDate);
                            Assert.AreEqual("UnderlyingTitle d570b00042d94321889af3c435f3652c", entity.UnderlyingTitle);
                            Assert.AreEqual(67655, entity.UnderlyingSharesAmount);
                            Assert.AreEqual(67655, entity.AmountFollowingReport);
                            Assert.AreEqual("NatureOfIndirectOwnership d570b00042d94321889af3c435f3652c", entity.NatureOfIndirectOwnership);
              
        }

        [TestCase("DerivativeTransaction\\030.Update.Success")]
        public void DerivativeTransaction_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDerivativeTransactionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            DerivativeTransaction entity = dal.Get(paramID);

                          entity.TitleOfDerivative = "TitleOfDerivative 91450a0a311744e39a8e6fadb41eb6f0";
                            entity.ConversionExercisePrice = 590079.540196M;
                            entity.TransactionDate = DateTime.Parse("10/14/2022 2:39:45 PM");
                            entity.TransactionCodeID = 9;
                            entity.EarlyVoluntarilyReport = false;              
                            entity.SharesAmount = 112505;
                            entity.DerivativeSecurityPrice = 112504.854385M;
                            entity.TransactionTypeID = 1;
                            entity.DateExercisable = DateTime.Parse("3/4/2020 12:26:45 AM");
                            entity.ExpirationDate = DateTime.Parse("3/4/2020 12:26:45 AM");
                            entity.UnderlyingTitle = "UnderlyingTitle 91450a0a311744e39a8e6fadb41eb6f0";
                            entity.UnderlyingSharesAmount = 112505;
                            entity.AmountFollowingReport = 112505;
                            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership 91450a0a311744e39a8e6fadb41eb6f0";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("TitleOfDerivative 91450a0a311744e39a8e6fadb41eb6f0", entity.TitleOfDerivative);
                            Assert.AreEqual(590079.540196M, entity.ConversionExercisePrice);
                            Assert.AreEqual(DateTime.Parse("10/14/2022 2:39:45 PM"), entity.TransactionDate);
                            Assert.AreEqual(9, entity.TransactionCodeID);
                            Assert.AreEqual(false, entity.EarlyVoluntarilyReport);
                            Assert.AreEqual(112505, entity.SharesAmount);
                            Assert.AreEqual(112504.854385M, entity.DerivativeSecurityPrice);
                            Assert.AreEqual(1, entity.TransactionTypeID);
                            Assert.AreEqual(DateTime.Parse("3/4/2020 12:26:45 AM"), entity.DateExercisable);
                            Assert.AreEqual(DateTime.Parse("3/4/2020 12:26:45 AM"), entity.ExpirationDate);
                            Assert.AreEqual("UnderlyingTitle 91450a0a311744e39a8e6fadb41eb6f0", entity.UnderlyingTitle);
                            Assert.AreEqual(112505, entity.UnderlyingSharesAmount);
                            Assert.AreEqual(112505, entity.AmountFollowingReport);
                            Assert.AreEqual("NatureOfIndirectOwnership 91450a0a311744e39a8e6fadb41eb6f0", entity.NatureOfIndirectOwnership);
              
        }

        [Test]
        public void DerivativeTransaction_Update_InvalidId()
        {
            var dal = PrepareDerivativeTransactionDal("DALInitParams");

            var entity = new DerivativeTransaction();
                          entity.TitleOfDerivative = "TitleOfDerivative 91450a0a311744e39a8e6fadb41eb6f0";
                            entity.ConversionExercisePrice = 590079.540196M;
                            entity.TransactionDate = DateTime.Parse("10/14/2022 2:39:45 PM");
                            entity.TransactionCodeID = 9;
                            entity.EarlyVoluntarilyReport = false;              
                            entity.SharesAmount = 112505;
                            entity.DerivativeSecurityPrice = 112504.854385M;
                            entity.TransactionTypeID = 1;
                            entity.DateExercisable = DateTime.Parse("3/4/2020 12:26:45 AM");
                            entity.ExpirationDate = DateTime.Parse("3/4/2020 12:26:45 AM");
                            entity.UnderlyingTitle = "UnderlyingTitle 91450a0a311744e39a8e6fadb41eb6f0";
                            entity.UnderlyingSharesAmount = 112505;
                            entity.AmountFollowingReport = 112505;
                            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership 91450a0a311744e39a8e6fadb41eb6f0";
              
            try
            {
                entity = dal.Update(entity);

                Assert.Fail("Fail - exception was expected, but wasn't thrown.");
            }
            catch (Exception ex)
            {
                Assert.Pass("Success - exception thrown as expected");
            }
        }


        protected IDerivativeTransactionDal PrepareDerivativeTransactionDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IDerivativeTransactionDal dal = new DerivativeTransactionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
