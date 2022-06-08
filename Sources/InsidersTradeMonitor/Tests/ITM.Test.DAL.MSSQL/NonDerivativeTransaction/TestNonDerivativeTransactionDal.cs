


using ITM.DAL.MSSQL;
using ITM.Interfaces;
using ITM.Interfaces.Entities;
using ITM.Test.Common;
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

            Assert.AreEqual(100026, entity.Form4ReportID);
            Assert.AreEqual("TitleOfSecurity c27ff82e6ff9424e814c5b98ee1c2f24", entity.TitleOfSecurity);
            Assert.AreEqual(DateTime.Parse("1/28/2023"), entity.TransactionDate);
            Assert.AreEqual(DateTime.Parse("1/28/2023"), entity.DeemedExecDate);
            Assert.AreEqual(14, entity.TransactionCodeID);
            Assert.AreEqual(false, entity.EarlyVoluntarilyReport);
            Assert.AreEqual(616652, entity.SharesAmount);
            Assert.AreEqual(1, entity.TransactionTypeID);
            Assert.AreEqual(616651.891552M, entity.Price);
            Assert.AreEqual(616652, entity.AmountFollowingReport);
            Assert.AreEqual(2, entity.OwnershipTypeID);
            Assert.AreEqual("NatureOfIndirectOwnership c27ff82e6ff9424e814c5b98ee1c2f24", entity.NatureOfIndirectOwnership);
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
            entity.Form4ReportID = 100016;
            entity.TitleOfSecurity = "TitleOfSecurity 8f1b249b767b44d6a82e2b583ccb4550";
            entity.TransactionDate = DateTime.Parse("4/27/2023");
            entity.DeemedExecDate = DateTime.Parse("4/27/2023");
            entity.TransactionCodeID = 11;
            entity.EarlyVoluntarilyReport = false;
            entity.SharesAmount = 661502;
            entity.TransactionTypeID = 1;
            entity.Price = 661502.51993M;
            entity.AmountFollowingReport = 661502;
            entity.OwnershipTypeID = 1;
            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership 8f1b249b767b44d6a82e2b583ccb4550";

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(100016, entity.Form4ReportID);
            Assert.AreEqual("TitleOfSecurity 8f1b249b767b44d6a82e2b583ccb4550", entity.TitleOfSecurity);
            Assert.AreEqual(DateTime.Parse("4/27/2023"), entity.TransactionDate);
            Assert.AreEqual(DateTime.Parse("4/27/2023"), entity.DeemedExecDate);
            Assert.AreEqual(11, entity.TransactionCodeID);
            Assert.AreEqual(false, entity.EarlyVoluntarilyReport);
            Assert.AreEqual(661502, entity.SharesAmount);
            Assert.AreEqual(1, entity.TransactionTypeID);
            Assert.AreEqual(661502.51993M, entity.Price);
            Assert.AreEqual(661502, entity.AmountFollowingReport);
            Assert.AreEqual(1, entity.OwnershipTypeID);
            Assert.AreEqual("NatureOfIndirectOwnership 8f1b249b767b44d6a82e2b583ccb4550", entity.NatureOfIndirectOwnership);

        }

        [TestCase("NonDerivativeTransaction\\030.Update.Success")]
        public void NonDerivativeTransaction_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareNonDerivativeTransactionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            NonDerivativeTransaction entity = dal.Get(paramID);

            entity.Form4ReportID = 100007;
            entity.TitleOfSecurity = "TitleOfSecurity ae48528ef3ce45c7ba52835f59ad6350";
            entity.TransactionDate = DateTime.Parse("9/13/2020");
            entity.DeemedExecDate = DateTime.Parse("9/13/2020");
            entity.TransactionCodeID = 4;
            entity.EarlyVoluntarilyReport = false;
            entity.SharesAmount = 706353;
            entity.TransactionTypeID = 2;
            entity.Price = 706353.148309M;
            entity.AmountFollowingReport = 706353;
            entity.OwnershipTypeID = 2;
            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership ae48528ef3ce45c7ba52835f59ad6350";

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(100007, entity.Form4ReportID);
            Assert.AreEqual("TitleOfSecurity ae48528ef3ce45c7ba52835f59ad6350", entity.TitleOfSecurity);
            Assert.AreEqual(DateTime.Parse("9/13/2020"), entity.TransactionDate);
            Assert.AreEqual(DateTime.Parse("9/13/2020"), entity.DeemedExecDate);
            Assert.AreEqual(4, entity.TransactionCodeID);
            Assert.AreEqual(false, entity.EarlyVoluntarilyReport);
            Assert.AreEqual(706353, entity.SharesAmount);
            Assert.AreEqual(2, entity.TransactionTypeID);
            Assert.AreEqual(706353.148309M, entity.Price);
            Assert.AreEqual(706353, entity.AmountFollowingReport);
            Assert.AreEqual(2, entity.OwnershipTypeID);
            Assert.AreEqual("NatureOfIndirectOwnership ae48528ef3ce45c7ba52835f59ad6350", entity.NatureOfIndirectOwnership);

        }

        [Test]
        public void NonDerivativeTransaction_Update_InvalidId()
        {
            var dal = PrepareNonDerivativeTransactionDal("DALInitParams");

            var entity = new NonDerivativeTransaction();
            entity.Form4ReportID = 100007;
            entity.TitleOfSecurity = "TitleOfSecurity ae48528ef3ce45c7ba52835f59ad6350";
            entity.TransactionDate = DateTime.Parse("9/13/2020");
            entity.DeemedExecDate = DateTime.Parse("9/13/2020");
            entity.TransactionCodeID = 4;
            entity.EarlyVoluntarilyReport = false;
            entity.SharesAmount = 706353;
            entity.TransactionTypeID = 2;
            entity.Price = 706353.148309M;
            entity.AmountFollowingReport = 706353;
            entity.OwnershipTypeID = 2;
            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership ae48528ef3ce45c7ba52835f59ad6350";

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
