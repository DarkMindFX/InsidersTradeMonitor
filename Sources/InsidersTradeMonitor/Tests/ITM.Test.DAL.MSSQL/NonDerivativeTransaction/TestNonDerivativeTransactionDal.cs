


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
    public class TestNonDerivativeTransactionDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            INonDerivativeTransactionDal dal = new NonDerivativeTransactionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void NonDerivativeTransaction_GetAll_Success()
        {
            var dal = PrepareNonDerivativeTransactionDal("DALInitParams");

            IList<NonDerivativeTransaction> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("NonDerivativeTransaction\\000.GetDetails.Success")]
        public void NonDerivativeTransaction_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareNonDerivativeTransactionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            NonDerivativeTransaction entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("TitleOfSecurity 80b9e2b0657c499fb4b00e444b82162a", entity.TitleOfSecurity);
                            Assert.AreEqual(DateTime.Parse("10/13/2019 8:14:45 AM"), entity.TransactionDate);
                            Assert.AreEqual(DateTime.Parse("10/13/2019 8:14:45 AM"), entity.DeemedExecDate);
                            Assert.AreEqual(2, entity.TransactionCodeID);
                            Assert.AreEqual(true, entity.EarlyVoluntarilyReport);
                            Assert.AreEqual(41082, entity.SharesAmount);
                            Assert.AreEqual(2, entity.TransactionTypeID);
                            Assert.AreEqual(563507.18884M, entity.Price);
                            Assert.AreEqual(563507, entity.AmountFollowingReport);
                            Assert.AreEqual("NatureOfIndirectOwnership 80b9e2b0657c499fb4b00e444b82162a", entity.NatureOfIndirectOwnership);
                      }

        [Test]
        public void NonDerivativeTransaction_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareNonDerivativeTransactionDal("DALInitParams");

            NonDerivativeTransaction entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("NonDerivativeTransaction\\010.Delete.Success")]
        public void NonDerivativeTransaction_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareNonDerivativeTransactionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void NonDerivativeTransaction_Delete_InvalidId()
        {
            var dal = PrepareNonDerivativeTransactionDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("NonDerivativeTransaction\\020.Insert.Success")]
        public void NonDerivativeTransaction_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareNonDerivativeTransactionDal("DALInitParams");

            var entity = new NonDerivativeTransaction();
                          entity.TitleOfSecurity = "TitleOfSecurity b7b1bd38ad73418388a9cda589639f2d";
                            entity.TransactionDate = DateTime.Parse("2/17/2023 2:28:45 PM");
                            entity.DeemedExecDate = DateTime.Parse("2/17/2023 2:28:45 PM");
                            entity.TransactionCodeID = 12;
                            entity.EarlyVoluntarilyReport = true;              
                            entity.SharesAmount = 653208;
                            entity.TransactionTypeID = 2;
                            entity.Price = 653208.445596M;
                            entity.AmountFollowingReport = 175634;
                            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership b7b1bd38ad73418388a9cda589639f2d";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("TitleOfSecurity b7b1bd38ad73418388a9cda589639f2d", entity.TitleOfSecurity);
                            Assert.AreEqual(DateTime.Parse("2/17/2023 2:28:45 PM"), entity.TransactionDate);
                            Assert.AreEqual(DateTime.Parse("2/17/2023 2:28:45 PM"), entity.DeemedExecDate);
                            Assert.AreEqual(12, entity.TransactionCodeID);
                            Assert.AreEqual(true, entity.EarlyVoluntarilyReport);
                            Assert.AreEqual(653208, entity.SharesAmount);
                            Assert.AreEqual(2, entity.TransactionTypeID);
                            Assert.AreEqual(653208.445596M, entity.Price);
                            Assert.AreEqual(175634, entity.AmountFollowingReport);
                            Assert.AreEqual("NatureOfIndirectOwnership b7b1bd38ad73418388a9cda589639f2d", entity.NatureOfIndirectOwnership);
              
        }

        [TestCase("NonDerivativeTransaction\\030.Update.Success")]
        public void NonDerivativeTransaction_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareNonDerivativeTransactionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            NonDerivativeTransaction entity = dal.Get(paramID);

                          entity.TitleOfSecurity = "TitleOfSecurity a783b1b8ad8243e182246b297d158e51";
                            entity.TransactionDate = DateTime.Parse("10/4/2020 10:28:45 AM");
                            entity.DeemedExecDate = DateTime.Parse("10/4/2020 10:28:45 AM");
                            entity.TransactionCodeID = 5;
                            entity.EarlyVoluntarilyReport = false;              
                            entity.SharesAmount = 220485;
                            entity.TransactionTypeID = 1;
                            entity.Price = 220484.388164M;
                            entity.AmountFollowingReport = 220485;
                            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership a783b1b8ad8243e182246b297d158e51";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("TitleOfSecurity a783b1b8ad8243e182246b297d158e51", entity.TitleOfSecurity);
                            Assert.AreEqual(DateTime.Parse("10/4/2020 10:28:45 AM"), entity.TransactionDate);
                            Assert.AreEqual(DateTime.Parse("10/4/2020 10:28:45 AM"), entity.DeemedExecDate);
                            Assert.AreEqual(5, entity.TransactionCodeID);
                            Assert.AreEqual(false, entity.EarlyVoluntarilyReport);
                            Assert.AreEqual(220485, entity.SharesAmount);
                            Assert.AreEqual(1, entity.TransactionTypeID);
                            Assert.AreEqual(220484.388164M, entity.Price);
                            Assert.AreEqual(220485, entity.AmountFollowingReport);
                            Assert.AreEqual("NatureOfIndirectOwnership a783b1b8ad8243e182246b297d158e51", entity.NatureOfIndirectOwnership);
              
        }

        [Test]
        public void NonDerivativeTransaction_Update_InvalidId()
        {
            var dal = PrepareNonDerivativeTransactionDal("DALInitParams");

            var entity = new NonDerivativeTransaction();
                          entity.TitleOfSecurity = "TitleOfSecurity a783b1b8ad8243e182246b297d158e51";
                            entity.TransactionDate = DateTime.Parse("10/4/2020 10:28:45 AM");
                            entity.DeemedExecDate = DateTime.Parse("10/4/2020 10:28:45 AM");
                            entity.TransactionCodeID = 5;
                            entity.EarlyVoluntarilyReport = false;              
                            entity.SharesAmount = 220485;
                            entity.TransactionTypeID = 1;
                            entity.Price = 220484.388164M;
                            entity.AmountFollowingReport = 220485;
                            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership a783b1b8ad8243e182246b297d158e51";
              
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


        protected INonDerivativeTransactionDal PrepareNonDerivativeTransactionDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            INonDerivativeTransactionDal dal = new NonDerivativeTransactionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
