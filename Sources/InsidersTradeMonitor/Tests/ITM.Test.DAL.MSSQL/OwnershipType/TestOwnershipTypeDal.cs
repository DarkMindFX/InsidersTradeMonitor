


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
    public class TestOwnershipTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IOwnershipTypeDal dal = new OwnershipTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void OwnershipType_GetAll_Success()
        {
            var dal = PrepareOwnershipTypeDal("DALInitParams");

            IList<OwnershipType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("OwnershipType\\000.GetDetails.Success")]
        public void OwnershipType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOwnershipTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OwnershipType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("C", entity.Code);
                            Assert.AreEqual("Description 65cd3c0b2ca8405c9747b0db27b0490a", entity.Description);
                      }

        [Test]
        public void OwnershipType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareOwnershipTypeDal("DALInitParams");

            OwnershipType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("OwnershipType\\010.Delete.Success")]
        public void OwnershipType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOwnershipTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void OwnershipType_Delete_InvalidId()
        {
            var dal = PrepareOwnershipTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("OwnershipType\\020.Insert.Success")]
        public void OwnershipType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareOwnershipTypeDal("DALInitParams");

            var entity = new OwnershipType();
                          entity.Code = "C";
                            entity.Description = "Description 0c96a714996e40b49ff7074e80564bfa";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("C", entity.Code);
                            Assert.AreEqual("Description 0c96a714996e40b49ff7074e80564bfa", entity.Description);
              
        }

        [TestCase("OwnershipType\\030.Update.Success")]
        public void OwnershipType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareOwnershipTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            OwnershipType entity = dal.Get(paramID);

                          entity.Code = "C";
                            entity.Description = "Description f73e3ed948bd4c17a5adb738ee8bcf3d";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("C", entity.Code);
                            Assert.AreEqual("Description f73e3ed948bd4c17a5adb738ee8bcf3d", entity.Description);
              
        }

        [Test]
        public void OwnershipType_Update_InvalidId()
        {
            var dal = PrepareOwnershipTypeDal("DALInitParams");

            var entity = new OwnershipType();
                          entity.Code = "C";
                            entity.Description = "Description f73e3ed948bd4c17a5adb738ee8bcf3d";
              
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


        protected IOwnershipTypeDal PrepareOwnershipTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IOwnershipTypeDal dal = new OwnershipTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
