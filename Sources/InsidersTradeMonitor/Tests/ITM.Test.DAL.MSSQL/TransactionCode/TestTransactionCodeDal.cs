


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
    public class TestTransactionCodeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ITransactionCodeDal dal = new TransactionCodeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void TransactionCode_GetAll_Success()
        {
            var dal = PrepareTransactionCodeDal("DALInitParams");

            IList<TransactionCode> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("TransactionCode\\000.GetDetails.Success")]
        public void TransactionCode_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareTransactionCodeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            TransactionCode entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.Code, Is.EqualTo("Code b4702"));
            Assert.That(entity.Description, Is.EqualTo("Description b47026094d5c4eb28d4e22f7bcd2f2cd"));
        }

        [Test]
        public void TransactionCode_GetDetails_InvalidId()
        {
            var paramID = Int64.MaxValue - 1;
            var dal = PrepareTransactionCodeDal("DALInitParams");

            TransactionCode entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("TransactionCode\\010.Delete.Success")]
        public void TransactionCode_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareTransactionCodeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void TransactionCode_Delete_InvalidId()
        {
            var dal = PrepareTransactionCodeDal("DALInitParams");
            var paramID = Int64.MaxValue - 1;

            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("TransactionCode\\020.Insert.Success")]
        public void TransactionCode_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareTransactionCodeDal("DALInitParams");

            var entity = new TransactionCode();
            entity.Code = "Code 6aa1d";
            entity.Description = "Description 6aa1d1627cf14760a39718c19b111b39";

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.Code, Is.EqualTo("Code 6aa1d"));
            Assert.That(entity.Description, Is.EqualTo("Description 6aa1d1627cf14760a39718c19b111b39"));

        }

        [TestCase("TransactionCode\\030.Update.Success")]
        public void TransactionCode_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareTransactionCodeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            TransactionCode entity = dal.Get(paramID);

            entity.Code = "Code 3b956";
            entity.Description = "Description 3b95600d0d1a434c8fd073c2b885b026";

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.Code, Is.EqualTo("Code 3b956"));
            Assert.That(entity.Description, Is.EqualTo("Description 3b95600d0d1a434c8fd073c2b885b026"));

        }

        [Test]
        public void TransactionCode_Update_InvalidId()
        {
            var dal = PrepareTransactionCodeDal("DALInitParams");

            var entity = new TransactionCode();
            entity.Code = "Code 3b956";
            entity.Description = "Description 3b95600d0d1a434c8fd073c2b885b026";

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


        protected ITransactionCodeDal PrepareTransactionCodeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ITransactionCodeDal dal = new TransactionCodeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
