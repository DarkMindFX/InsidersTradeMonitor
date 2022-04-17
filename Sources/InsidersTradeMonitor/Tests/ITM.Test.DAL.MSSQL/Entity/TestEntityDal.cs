


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
    public class TestEntityDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IEntityDal dal = new EntityDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Entity_GetAll_Success()
        {
            var dal = PrepareEntityDal("DALInitParams");

            IList<Entity> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Entity\\000.GetDetails.Success")]
        public void Entity_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareEntityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64)objIds[0];
            Entity entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(323472, entity.ID);
                            Assert.AreEqual("CIK d1d8f4bf320246ea84b8233bd50ddaaf", entity.CIK);
                            Assert.AreEqual("Name d1d8f4bf320246ea84b8233bd50ddaaf", entity.Name);
                            Assert.AreEqual("TradingSymbol d1d8f4bf320246ea84b8233bd50ddaaf", entity.TradingSymbol);
                      }

        [Test]
        public void Entity_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareEntityDal("DALInitParams");

            Entity entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Entity\\010.Delete.Success")]
        public void Entity_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareEntityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Entity_Delete_InvalidId()
        {
            var dal = PrepareEntityDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Entity\\020.Insert.Success")]
        public void Entity_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareEntityDal("DALInitParams");

            var entity = new Entity();
                          entity.ID = 890747;
                            entity.CIK = "CIK 6040a272638844719fda814739096578";
                            entity.Name = "Name 6040a272638844719fda814739096578";
                            entity.TradingSymbol = "TradingSymbol 6040a272638844719fda814739096578";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(890747, entity.ID);
                            Assert.AreEqual("CIK 6040a272638844719fda814739096578", entity.CIK);
                            Assert.AreEqual("Name 6040a272638844719fda814739096578", entity.Name);
                            Assert.AreEqual("TradingSymbol 6040a272638844719fda814739096578", entity.TradingSymbol);
              
        }

        [TestCase("Entity\\030.Update.Success")]
        public void Entity_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareEntityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64)objIds[0];
            Entity entity = dal.Get(paramID);

                          entity.CIK = "CIK 948ff2c72e1e4918af8e10dc67ed125b";
                            entity.Name = "Name 948ff2c72e1e4918af8e10dc67ed125b";
                            entity.TradingSymbol = "TradingSymbol 948ff2c72e1e4918af8e10dc67ed125b";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(890747, entity.ID);
                            Assert.AreEqual("CIK 948ff2c72e1e4918af8e10dc67ed125b", entity.CIK);
                            Assert.AreEqual("Name 948ff2c72e1e4918af8e10dc67ed125b", entity.Name);
                            Assert.AreEqual("TradingSymbol 948ff2c72e1e4918af8e10dc67ed125b", entity.TradingSymbol);
              
        }

        [Test]
        public void Entity_Update_InvalidId()
        {
            var dal = PrepareEntityDal("DALInitParams");

            var entity = new Entity();
                          entity.ID = 890747;
                            entity.CIK = "CIK 948ff2c72e1e4918af8e10dc67ed125b";
                            entity.Name = "Name 948ff2c72e1e4918af8e10dc67ed125b";
                            entity.TradingSymbol = "TradingSymbol 948ff2c72e1e4918af8e10dc67ed125b";
              
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


        protected IEntityDal PrepareEntityDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IEntityDal dal = new EntityDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
