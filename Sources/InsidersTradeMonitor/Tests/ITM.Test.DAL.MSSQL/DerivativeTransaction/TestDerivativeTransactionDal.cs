


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

            Assert.AreEqual(100007, entity.Form4ReportID);
            Assert.AreEqual("TitleOfDerivative 735dce9f4ebe44a9a11bd76db27a4745", entity.TitleOfDerivative);
            Assert.AreEqual(181431.783448M, entity.ConversionExercisePrice);
            Assert.AreEqual(DateTime.Parse("9/9/2020"), entity.TransactionDate);
            Assert.AreEqual(15, entity.TransactionCodeID);
            Assert.AreEqual(true, entity.EarlyVoluntarilyReport);
            Assert.AreEqual(226283, entity.SharesAmount);
            Assert.AreEqual(226282.411826M, entity.DerivativeSecurityPrice);
            Assert.AreEqual(2, entity.TransactionTypeID);
            Assert.AreEqual(DateTime.Parse("10/19/2023"), entity.DateExercisable);
            Assert.AreEqual(DateTime.Parse("10/19/2023"), entity.ExpirationDate);
            Assert.AreEqual("UnderlyingTitle 735dce9f4ebe44a9a11bd76db27a4745", entity.UnderlyingTitle);
            Assert.AreEqual(748707, entity.UnderlyingSharesAmount);
            Assert.AreEqual(748707, entity.AmountFollowingReport);
            Assert.AreEqual(2, entity.OwnershipTypeID);
            Assert.AreEqual("NatureOfIndirectOwnership 735dce9f4ebe44a9a11bd76db27a4745", entity.NatureOfIndirectOwnership);
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
            entity.Form4ReportID = 100013;
            entity.TitleOfDerivative = "TitleOfDerivative 82799dcf64f9462791f901cd16b3424c";
            entity.ConversionExercisePrice = 793558.354393M;
            entity.TransactionDate = DateTime.Parse("1/16/2024");
            entity.TransactionCodeID = 11;
            entity.EarlyVoluntarilyReport = false;
            entity.SharesAmount = 315984;
            entity.DerivativeSecurityPrice = 315983.668583M;
            entity.TransactionTypeID = 1;
            entity.DateExercisable = DateTime.Parse("6/5/2021");
            entity.ExpirationDate = DateTime.Parse("6/5/2021");
            entity.UnderlyingTitle = "UnderlyingTitle 82799dcf64f9462791f901cd16b3424c";
            entity.UnderlyingSharesAmount = 315984;
            entity.AmountFollowingReport = 315984;
            entity.OwnershipTypeID = 1;
            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership 82799dcf64f9462791f901cd16b3424c";

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(100013, entity.Form4ReportID);
            Assert.AreEqual("TitleOfDerivative 82799dcf64f9462791f901cd16b3424c", entity.TitleOfDerivative);
            Assert.AreEqual(793558.354393M, entity.ConversionExercisePrice);
            Assert.AreEqual(DateTime.Parse("1/16/2024"), entity.TransactionDate);
            Assert.AreEqual(11, entity.TransactionCodeID);
            Assert.AreEqual(false, entity.EarlyVoluntarilyReport);
            Assert.AreEqual(315984, entity.SharesAmount);
            Assert.AreEqual(315983.668583M, entity.DerivativeSecurityPrice);
            Assert.AreEqual(1, entity.TransactionTypeID);
            Assert.AreEqual(DateTime.Parse("6/5/2021"), entity.DateExercisable);
            Assert.AreEqual(DateTime.Parse("6/5/2021"), entity.ExpirationDate);
            Assert.AreEqual("UnderlyingTitle 82799dcf64f9462791f901cd16b3424c", entity.UnderlyingTitle);
            Assert.AreEqual(315984, entity.UnderlyingSharesAmount);
            Assert.AreEqual(315984, entity.AmountFollowingReport);
            Assert.AreEqual(1, entity.OwnershipTypeID);
            Assert.AreEqual("NatureOfIndirectOwnership 82799dcf64f9462791f901cd16b3424c", entity.NatureOfIndirectOwnership);

        }

        [TestCase("DerivativeTransaction\\030.Update.Success")]
        public void DerivativeTransaction_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDerivativeTransactionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            DerivativeTransaction entity = dal.Get(paramID);

            entity.Form4ReportID = 100019;
            entity.TitleOfDerivative = "TitleOfDerivative 664806d526eb4758ac9d975bb2ca7419";
            entity.ConversionExercisePrice = 315983.668583M;
            entity.TransactionDate = DateTime.Parse("6/5/2021");
            entity.TransactionCodeID = 2;
            entity.EarlyVoluntarilyReport = false;
            entity.SharesAmount = 838409;
            entity.DerivativeSecurityPrice = 838408.982772M;
            entity.TransactionTypeID = 1;
            entity.DateExercisable = DateTime.Parse("4/15/2024");
            entity.ExpirationDate = DateTime.Parse("4/15/2024");
            entity.UnderlyingTitle = "UnderlyingTitle 664806d526eb4758ac9d975bb2ca7419";
            entity.UnderlyingSharesAmount = 838409;
            entity.AmountFollowingReport = 838409;
            entity.OwnershipTypeID = 1;
            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership 664806d526eb4758ac9d975bb2ca7419";

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(100019, entity.Form4ReportID);
            Assert.AreEqual("TitleOfDerivative 664806d526eb4758ac9d975bb2ca7419", entity.TitleOfDerivative);
            Assert.AreEqual(315983.668583M, entity.ConversionExercisePrice);
            Assert.AreEqual(DateTime.Parse("6/5/2021"), entity.TransactionDate);
            Assert.AreEqual(2, entity.TransactionCodeID);
            Assert.AreEqual(false, entity.EarlyVoluntarilyReport);
            Assert.AreEqual(838409, entity.SharesAmount);
            Assert.AreEqual(838408.982772M, entity.DerivativeSecurityPrice);
            Assert.AreEqual(1, entity.TransactionTypeID);
            Assert.AreEqual(DateTime.Parse("4/15/2024"), entity.DateExercisable);
            Assert.AreEqual(DateTime.Parse("4/15/2024"), entity.ExpirationDate);
            Assert.AreEqual("UnderlyingTitle 664806d526eb4758ac9d975bb2ca7419", entity.UnderlyingTitle);
            Assert.AreEqual(838409, entity.UnderlyingSharesAmount);
            Assert.AreEqual(838409, entity.AmountFollowingReport);
            Assert.AreEqual(1, entity.OwnershipTypeID);
            Assert.AreEqual("NatureOfIndirectOwnership 664806d526eb4758ac9d975bb2ca7419", entity.NatureOfIndirectOwnership);

        }

        [Test]
        public void DerivativeTransaction_Update_InvalidId()
        {
            var dal = PrepareDerivativeTransactionDal("DALInitParams");

            var entity = new DerivativeTransaction();
            entity.Form4ReportID = 100019;
            entity.TitleOfDerivative = "TitleOfDerivative 664806d526eb4758ac9d975bb2ca7419";
            entity.ConversionExercisePrice = 315983.668583M;
            entity.TransactionDate = DateTime.Parse("6/5/2021");
            entity.TransactionCodeID = 2;
            entity.EarlyVoluntarilyReport = false;
            entity.SharesAmount = 838409;
            entity.DerivativeSecurityPrice = 838408.982772M;
            entity.TransactionTypeID = 1;
            entity.DateExercisable = DateTime.Parse("4/15/2024");
            entity.ExpirationDate = DateTime.Parse("4/15/2024");
            entity.UnderlyingTitle = "UnderlyingTitle 664806d526eb4758ac9d975bb2ca7419";
            entity.UnderlyingSharesAmount = 838409;
            entity.AmountFollowingReport = 838409;
            entity.OwnershipTypeID = 1;
            entity.NatureOfIndirectOwnership = "NatureOfIndirectOwnership 664806d526eb4758ac9d975bb2ca7419";

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
