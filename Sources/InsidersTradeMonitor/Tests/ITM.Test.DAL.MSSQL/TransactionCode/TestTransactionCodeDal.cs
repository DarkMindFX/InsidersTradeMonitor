


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
            
                          Assert.AreEqual("Code c12fb", entity.Code);
                            Assert.AreEqual("Description c12fb2a8b1f2434ca516a48ee1e5534e", entity.Description);
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
                          entity.Code = "Code a8a7c";
                            entity.Description = "Description a8a7ceb8af0d40d3b4ba841bfcc1cfbe";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Code a8a7c", entity.Code);
                            Assert.AreEqual("Description a8a7ceb8af0d40d3b4ba841bfcc1cfbe", entity.Description);
              
        }

        [TestCase("TransactionCode\\030.Update.Success")]
        public void TransactionCode_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareTransactionCodeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            TransactionCode entity = dal.Get(paramID);

                          entity.Code = "Code 235d0";
                            entity.Description = "Description 235d01e9705f4e059f295a874cdc0642";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Code 235d0", entity.Code);
                            Assert.AreEqual("Description 235d01e9705f4e059f295a874cdc0642", entity.Description);
              
        }

        [Test]
        public void TransactionCode_Update_InvalidId()
        {
            var dal = PrepareTransactionCodeDal("DALInitParams");

            var entity = new TransactionCode();
                          entity.Code = "Code 235d0";
                            entity.Description = "Description 235d01e9705f4e059f295a874cdc0642";
              
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
