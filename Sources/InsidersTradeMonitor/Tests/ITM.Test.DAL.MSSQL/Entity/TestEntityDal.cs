


using ITM.DAL.MSSQL;
using ITM.Interfaces;
using ITM.Interfaces.Entities;
using ITM.Test.Common;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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
            var paramID = (System.Int64?)objIds[0];
            Entity entity = dal.Get((long)paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(1, entity.EntityTypeID);
            Assert.AreEqual(735, entity.CIK);
            Assert.AreEqual("Name b0f5c74af5374d68b863a3207a175683", entity.Name);
            Assert.AreEqual("TradingSymbol b0f5c74af5374d68b863a3207a175683", entity.TradingSymbol);
            Assert.AreEqual(true, entity.IsMonitored);
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
            var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete((long)paramID);

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
            entity.EntityTypeID = 1;
            entity.CIK = 258;
            entity.Name = "Name 967135663aa542908c8c518c3ddd06b5";
            entity.TradingSymbol = "TradingSymbol 967135663aa542908c8c518c3ddd06b5";
            entity.IsMonitored = false;

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(1, entity.EntityTypeID);
            Assert.AreEqual(258, entity.CIK);
            Assert.AreEqual("Name 967135663aa542908c8c518c3ddd06b5", entity.Name);
            Assert.AreEqual("TradingSymbol 967135663aa542908c8c518c3ddd06b5", entity.TradingSymbol);
            Assert.AreEqual(false, entity.IsMonitored);

        }

        [TestCase("Entity\\030.Update.Success")]
        public void Entity_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareEntityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            Entity entity = dal.Get((long)paramID);

            entity.EntityTypeID = 1;
            entity.CIK = 258;
            entity.Name = "Name 7df99239cc0f46e48a6ae2c439012918";
            entity.TradingSymbol = "TradingSymbol 7df99239cc0f46e48a6ae2c439012918";
            entity.IsMonitored = false;

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(1, entity.EntityTypeID);
            Assert.AreEqual(258, entity.CIK);
            Assert.AreEqual("Name 7df99239cc0f46e48a6ae2c439012918", entity.Name);
            Assert.AreEqual("TradingSymbol 7df99239cc0f46e48a6ae2c439012918", entity.TradingSymbol);
            Assert.AreEqual(false, entity.IsMonitored);

        }

        [Test]
        public void Entity_Update_InvalidId()
        {
            var dal = PrepareEntityDal("DALInitParams");

            var entity = new Entity();
            entity.EntityTypeID = 1;
            entity.CIK = 258;
            entity.Name = "Name 7df99239cc0f46e48a6ae2c439012918";
            entity.TradingSymbol = "TradingSymbol 7df99239cc0f46e48a6ae2c439012918";
            entity.IsMonitored = false;

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

        [TestCase("Entity\\040.GetMonitoredList.Success")]
        public void Entity_GetMonitoredList_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareEntityDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];

            var entities = dal.GetMonitoredList();

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
            Assert.IsNotEmpty(entities.Where(e => e.IsMonitored == true));
            Assert.IsEmpty(entities.Where(e => e.IsMonitored == false));
            Assert.IsNotNull(entities.FirstOrDefault( e => e.ID == paramID));
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
