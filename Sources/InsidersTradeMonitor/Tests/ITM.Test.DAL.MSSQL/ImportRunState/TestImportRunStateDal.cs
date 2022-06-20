


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
    public class TestImportRunStateDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IImportRunStateDal dal = new ImportRunStateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ImportRunState_GetAll_Success()
        {
            var dal = PrepareImportRunStateDal("DALInitParams");

            IList<ImportRunState> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ImportRunState\\000.GetDetails.Success")]
        public void ImportRunState_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImportRunStateDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ImportRunState entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name fef168bb5d3f4b8486b2e7b3330fbdc9", entity.Name);
                      }

        [Test]
        public void ImportRunState_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareImportRunStateDal("DALInitParams");

            ImportRunState entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("ImportRunState\\010.Delete.Success")]
        public void ImportRunState_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImportRunStateDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ImportRunState_Delete_InvalidId()
        {
            var dal = PrepareImportRunStateDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("ImportRunState\\020.Insert.Success")]
        public void ImportRunState_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareImportRunStateDal("DALInitParams");

            var entity = new ImportRunState();
                          entity.Name = "Name c28ca28c93c141f5ac7c1442e11a70a1";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name c28ca28c93c141f5ac7c1442e11a70a1", entity.Name);
              
        }

        [TestCase("ImportRunState\\030.Update.Success")]
        public void ImportRunState_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImportRunStateDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ImportRunState entity = dal.Get(paramID);

                          entity.Name = "Name 0d547fa30c4946d8ad1d76bff0aeebf5";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 0d547fa30c4946d8ad1d76bff0aeebf5", entity.Name);
              
        }

        [Test]
        public void ImportRunState_Update_InvalidId()
        {
            var dal = PrepareImportRunStateDal("DALInitParams");

            var entity = new ImportRunState();
                          entity.Name = "Name 0d547fa30c4946d8ad1d76bff0aeebf5";
              
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


        protected IImportRunStateDal PrepareImportRunStateDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IImportRunStateDal dal = new ImportRunStateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
