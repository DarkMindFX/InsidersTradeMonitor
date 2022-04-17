


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
    public class TestTransactionTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ITransactionTypeDal dal = new TransactionTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void TransactionType_GetAll_Success()
        {
            var dal = PrepareTransactionTypeDal("DALInitParams");

            IList<TransactionType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("TransactionType\\000.GetDetails.Success")]
        public void TransactionType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareTransactionTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            TransactionType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Code 2a3a1", entity.Code);
                            Assert.AreEqual("Description 2a3a13feb5424105b6dabe0873934b8b", entity.Description);
                      }

        [Test]
        public void TransactionType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareTransactionTypeDal("DALInitParams");

            TransactionType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("TransactionType\\010.Delete.Success")]
        public void TransactionType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareTransactionTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void TransactionType_Delete_InvalidId()
        {
            var dal = PrepareTransactionTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("TransactionType\\020.Insert.Success")]
        public void TransactionType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareTransactionTypeDal("DALInitParams");

            var entity = new TransactionType();
                          entity.Code = "Code 45b88";
                            entity.Description = "Description 45b88d21fc5048b3a6a94d2876e7b621";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Code 45b88", entity.Code);
                            Assert.AreEqual("Description 45b88d21fc5048b3a6a94d2876e7b621", entity.Description);
              
        }

        [TestCase("TransactionType\\030.Update.Success")]
        public void TransactionType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareTransactionTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            TransactionType entity = dal.Get(paramID);

                          entity.Code = "Code e7c33";
                            entity.Description = "Description e7c337dda94b46c5bea5d7730fc8eda5";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Code e7c33", entity.Code);
                            Assert.AreEqual("Description e7c337dda94b46c5bea5d7730fc8eda5", entity.Description);
              
        }

        [Test]
        public void TransactionType_Update_InvalidId()
        {
            var dal = PrepareTransactionTypeDal("DALInitParams");

            var entity = new TransactionType();
                          entity.Code = "Code e7c33";
                            entity.Description = "Description e7c337dda94b46c5bea5d7730fc8eda5";
              
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


        protected ITransactionTypeDal PrepareTransactionTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ITransactionTypeDal dal = new TransactionTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
