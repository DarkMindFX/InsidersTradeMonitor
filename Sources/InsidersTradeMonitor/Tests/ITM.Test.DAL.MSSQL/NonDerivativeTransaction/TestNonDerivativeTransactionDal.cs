


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

            Assert.That(entity.Form4ReportID, Is.EqualTo(100026));
            Assert.That(entity.TitleOfSecurity, Is.EqualTo("TitleOfSecurity c27ff82e6ff9424e814c5b98ee1c2f24"));
            Assert.That(entity.TransactionDate, Is.EqualTo(DateTime.Parse("1/28/2023")));
            Assert.That(entity.DeemedExecDate, Is.EqualTo(DateTime.Parse("1/28/2023")));
            Assert.That(entity.TransactionCodeID, Is.EqualTo(14));
            Assert.That(entity.EarlyVoluntarilyReport, Is.EqualTo(false));
            Assert.That(entity.SharesAmount, Is.EqualTo(616652));
            Assert.That(entity.TransactionTypeID, Is.EqualTo(1));
            Assert.That(entity.Price, Is.EqualTo(616651.891552M));
            Assert.That(entity.AmountFollowingReport, Is.EqualTo(616652));
            Assert.That(entity.OwnershipTypeID, Is.EqualTo(2));
            Assert.That(entity.NatureOfIndirectOwnership, Is.EqualTo("NatureOfIndirectOwnership c27ff82e6ff9424e814c5b98ee1c2f24"));
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

            Assert.That(entity.Form4ReportID, Is.EqualTo(100016));
            Assert.That(entity.TitleOfSecurity, Is.EqualTo("TitleOfSecurity 8f1b249b767b44d6a82e2b583ccb4550"));
            Assert.That(entity.TransactionDate, Is.EqualTo(DateTime.Parse("4/27/2023")));
            Assert.That(entity.DeemedExecDate, Is.EqualTo(DateTime.Parse("4/27/2023")));
            Assert.That(entity.TransactionCodeID, Is.EqualTo(11));
            Assert.That(entity.EarlyVoluntarilyReport, Is.EqualTo(false));
            Assert.That(entity.SharesAmount, Is.EqualTo(661502));
            Assert.That(entity.TransactionTypeID, Is.EqualTo(1));
            Assert.That(entity.Price, Is.EqualTo(661502.51993M));
            Assert.That(entity.AmountFollowingReport, Is.EqualTo(661502));
            Assert.That(entity.OwnershipTypeID, Is.EqualTo(1));
            Assert.That(entity.NatureOfIndirectOwnership, Is.EqualTo("NatureOfIndirectOwnership 8f1b249b767b44d6a82e2b583ccb4550"));

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

            Assert.That(entity.Form4ReportID, Is.EqualTo(100007));
            Assert.That(entity.TitleOfSecurity, Is.EqualTo("TitleOfSecurity ae48528ef3ce45c7ba52835f59ad6350"));
            Assert.That(entity.TransactionDate, Is.EqualTo(DateTime.Parse("9/13/2020")));
            Assert.That(entity.DeemedExecDate, Is.EqualTo(DateTime.Parse("9/13/2020")));
            Assert.That(entity.TransactionCodeID, Is.EqualTo(4));
            Assert.That(entity.EarlyVoluntarilyReport, Is.EqualTo(false));
            Assert.That(entity.SharesAmount, Is.EqualTo(706353));
            Assert.That(entity.TransactionTypeID, Is.EqualTo(2));
            Assert.That(entity.Price, Is.EqualTo(706353.148309M));
            Assert.That(entity.AmountFollowingReport, Is.EqualTo(706353));
            Assert.That(entity.OwnershipTypeID, Is.EqualTo(2));
            Assert.That(entity.NatureOfIndirectOwnership, Is.EqualTo("NatureOfIndirectOwnership ae48528ef3ce45c7ba52835f59ad6350"));

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
