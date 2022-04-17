


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
    public class TestSecurityTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ISecurityTypeDal dal = new SecurityTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void SecurityType_GetAll_Success()
        {
            var dal = PrepareSecurityTypeDal("DALInitParams");

            IList<SecurityType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("SecurityType\\000.GetDetails.Success")]
        public void SecurityType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSecurityTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            SecurityType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("SecurityTypeName 2c9f447c161642fdad1aa127fbc1f9c8", entity.SecurityTypeName);
                      }

        [Test]
        public void SecurityType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareSecurityTypeDal("DALInitParams");

            SecurityType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("SecurityType\\010.Delete.Success")]
        public void SecurityType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSecurityTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void SecurityType_Delete_InvalidId()
        {
            var dal = PrepareSecurityTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("SecurityType\\020.Insert.Success")]
        public void SecurityType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareSecurityTypeDal("DALInitParams");

            var entity = new SecurityType();
                          entity.SecurityTypeName = "SecurityTypeName 2e970d006ec34a84a4ffcc21da5a4dbd";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("SecurityTypeName 2e970d006ec34a84a4ffcc21da5a4dbd", entity.SecurityTypeName);
              
        }

        [TestCase("SecurityType\\030.Update.Success")]
        public void SecurityType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSecurityTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            SecurityType entity = dal.Get(paramID);

                          entity.SecurityTypeName = "SecurityTypeName b01565710db6487b823f567474335ba7";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("SecurityTypeName b01565710db6487b823f567474335ba7", entity.SecurityTypeName);
              
        }

        [Test]
        public void SecurityType_Update_InvalidId()
        {
            var dal = PrepareSecurityTypeDal("DALInitParams");

            var entity = new SecurityType();
                          entity.SecurityTypeName = "SecurityTypeName b01565710db6487b823f567474335ba7";
              
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


        protected ISecurityTypeDal PrepareSecurityTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ISecurityTypeDal dal = new SecurityTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
