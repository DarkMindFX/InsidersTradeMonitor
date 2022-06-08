


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
    public class TestEntityTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IEntityTypeDal dal = new EntityTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void EntityType_GetAll_Success()
        {
            var dal = PrepareEntityTypeDal("DALInitParams");

            IList<EntityType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("EntityType\\000.GetDetails.Success")]
        public void EntityType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareEntityTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            EntityType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("TypeName 8ea83002c9a54e2a81c3886644b8bc2f", entity.TypeName);
                      }

        [Test]
        public void EntityType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareEntityTypeDal("DALInitParams");

            EntityType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("EntityType\\010.Delete.Success")]
        public void EntityType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareEntityTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void EntityType_Delete_InvalidId()
        {
            var dal = PrepareEntityTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("EntityType\\020.Insert.Success")]
        public void EntityType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareEntityTypeDal("DALInitParams");

            var entity = new EntityType();
                          entity.TypeName = "TypeName d8f9da4dbee3422eb45828ecda25bbc7";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("TypeName d8f9da4dbee3422eb45828ecda25bbc7", entity.TypeName);
              
        }

        [TestCase("EntityType\\030.Update.Success")]
        public void EntityType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareEntityTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            EntityType entity = dal.Get(paramID);

                          entity.TypeName = "TypeName 4900b709aeed475f9bdd20dc927f2bb1";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("TypeName 4900b709aeed475f9bdd20dc927f2bb1", entity.TypeName);
              
        }

        [Test]
        public void EntityType_Update_InvalidId()
        {
            var dal = PrepareEntityTypeDal("DALInitParams");

            var entity = new EntityType();
                          entity.TypeName = "TypeName 4900b709aeed475f9bdd20dc927f2bb1";
              
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


        protected IEntityTypeDal PrepareEntityTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IEntityTypeDal dal = new EntityTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
