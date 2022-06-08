


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
            
                          Assert.AreEqual("Code 926f1", entity.Code);
                            Assert.AreEqual("Description 926f19be56db42dbbc6c83009834c264", entity.Description);
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
                          entity.Code = "Code 31b65";
                            entity.Description = "Description 31b65ad02809464590c1dfa2a46b2b4e";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Code 31b65", entity.Code);
                            Assert.AreEqual("Description 31b65ad02809464590c1dfa2a46b2b4e", entity.Description);
              
        }

        [TestCase("TransactionType\\030.Update.Success")]
        public void TransactionType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareTransactionTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            TransactionType entity = dal.Get(paramID);

                          entity.Code = "Code 6d517";
                            entity.Description = "Description 6d517bbe8f374571ab4ea5dbb6ffdd5b";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Code 6d517", entity.Code);
                            Assert.AreEqual("Description 6d517bbe8f374571ab4ea5dbb6ffdd5b", entity.Description);
              
        }

        [Test]
        public void TransactionType_Update_InvalidId()
        {
            var dal = PrepareTransactionTypeDal("DALInitParams");

            var entity = new TransactionType();
                          entity.Code = "Code 6d517";
                            entity.Description = "Description 6d517bbe8f374571ab4ea5dbb6ffdd5b";
              
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
