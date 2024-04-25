


using ITM.DAL.MSSQL;
using ITM.Interfaces;
using ITM.Interfaces.Entities;
using ITM.Test.Common;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Test.ITM.DAL.MSSQL
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

            Assert.That(entity.Form4ReportID, Is.EqualTo(100007));
            Assert.That(entity.TitleOfDerivative, Is.EqualTo("TitleOfDerivative 735dce9f4ebe44a9a11bd76db27a4745"));
            Assert.That(entity.ConversionExercisePrice, Is.EqualTo(181431.783448M));
            Assert.That(entity.TransactionDate, Is.EqualTo(DateTime.Parse("9/9/2020")));
            Assert.That(entity.TransactionCodeID, Is.EqualTo(15));
            Assert.That(entity.EarlyVoluntarilyReport, Is.EqualTo(true));
            Assert.That(entity.SharesAmount, Is.EqualTo(226283));
            Assert.That(entity.DerivativeSecurityPrice, Is.EqualTo(226282.411826M));
            Assert.That(entity.TransactionTypeID, Is.EqualTo(2));
            Assert.That(entity.DateExercisable, Is.EqualTo(DateTime.Parse("10/19/2023")));
            Assert.That(entity.ExpirationDate, Is.EqualTo(DateTime.Parse("10/19/2023")));
            Assert.That(entity.UnderlyingTitle, Is.EqualTo("UnderlyingTitle 735dce9f4ebe44a9a11bd76db27a4745"));
            Assert.That(entity.UnderlyingSharesAmount, Is.EqualTo(748707));
            Assert.That(entity.AmountFollowingReport, Is.EqualTo(748707));
            Assert.That(entity.OwnershipTypeID, Is.EqualTo(2));
            Assert.That(entity.NatureOfIndirectOwnership, Is.EqualTo("NatureOfIndirectOwnership 735dce9f4ebe44a9a11bd76db27a4745"));
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

            Assert.That(entity.Form4ReportID, Is.EqualTo(100013));
            Assert.That(entity.TitleOfDerivative, Is.EqualTo("TitleOfDerivative 82799dcf64f9462791f901cd16b3424c"));
            Assert.That(entity.ConversionExercisePrice, Is.EqualTo(793558.354393M));
            Assert.That(entity.TransactionDate, Is.EqualTo(DateTime.Parse("1/16/2024")));
            Assert.That(entity.TransactionCodeID, Is.EqualTo(11));
            Assert.That(entity.EarlyVoluntarilyReport, Is.EqualTo(false));
            Assert.That(entity.SharesAmount, Is.EqualTo(315984));
            Assert.That(entity.DerivativeSecurityPrice, Is.EqualTo(315983.668583M));
            Assert.That(entity.TransactionTypeID, Is.EqualTo(1));
            Assert.That(entity.DateExercisable, Is.EqualTo(DateTime.Parse("6/5/2021")));
            Assert.That(entity.ExpirationDate, Is.EqualTo(DateTime.Parse("6/5/2021")));
            Assert.That(entity.UnderlyingTitle, Is.EqualTo("UnderlyingTitle 82799dcf64f9462791f901cd16b3424c"));
            Assert.That(entity.UnderlyingSharesAmount, Is.EqualTo(315984));
            Assert.That(entity.AmountFollowingReport, Is.EqualTo(315984));
            Assert.That(entity.OwnershipTypeID, Is.EqualTo(1));
            Assert.That(entity.NatureOfIndirectOwnership, Is.EqualTo("NatureOfIndirectOwnership 82799dcf64f9462791f901cd16b3424c"));

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

            Assert.That(entity.Form4ReportID, Is.EqualTo(100019));
            Assert.That(entity.TitleOfDerivative, Is.EqualTo("TitleOfDerivative 664806d526eb4758ac9d975bb2ca7419"));
            Assert.That(entity.ConversionExercisePrice, Is.EqualTo(315983.668583M));
            Assert.That(entity.TransactionDate, Is.EqualTo(DateTime.Parse("6/5/2021")));
            Assert.That(entity.TransactionCodeID, Is.EqualTo(2));
            Assert.That(entity.EarlyVoluntarilyReport, Is.EqualTo(false));
            Assert.That(entity.SharesAmount, Is.EqualTo(838409));
            Assert.That(entity.DerivativeSecurityPrice, Is.EqualTo(838408.982772M));
            Assert.That(entity.TransactionTypeID, Is.EqualTo(1));
            Assert.That(entity.DateExercisable, Is.EqualTo(DateTime.Parse("4/15/2024")));
            Assert.That(entity.ExpirationDate, Is.EqualTo(DateTime.Parse("4/15/2024")));
            Assert.That(entity.UnderlyingTitle, Is.EqualTo("UnderlyingTitle 664806d526eb4758ac9d975bb2ca7419"));
            Assert.That(entity.UnderlyingSharesAmount, Is.EqualTo(838409));
            Assert.That(entity.AmountFollowingReport, Is.EqualTo(838409));
            Assert.That(entity.OwnershipTypeID, Is.EqualTo(1));
            Assert.That(entity.NatureOfIndirectOwnership, Is.EqualTo("NatureOfIndirectOwnership 664806d526eb4758ac9d975bb2ca7419"));

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
