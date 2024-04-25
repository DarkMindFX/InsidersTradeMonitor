


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

            Assert.That(entity.Code, Is.EqualTo("C"));
            Assert.That(entity.Description, Is.EqualTo("Description 0635c222641c4d3f884406c2c7709082"));
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
            entity.Description = "Description 551331c4138e443da5ae6323f444af8f";

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.Code, Is.EqualTo("C"));
            Assert.That(entity.Description, Is.EqualTo("Description 551331c4138e443da5ae6323f444af8f"));

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
            entity.Description = "Description fab82e3caa4f4bf9aaf114a58bf57025";

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.Code, Is.EqualTo("C"));
            Assert.That(entity.Description, Is.EqualTo("Description fab82e3caa4f4bf9aaf114a58bf57025"));

        }

        [Test]
        public void OwnershipType_Update_InvalidId()
        {
            var dal = PrepareOwnershipTypeDal("DALInitParams");

            var entity = new OwnershipType();
            entity.Code = "C";
            entity.Description = "Description fab82e3caa4f4bf9aaf114a58bf57025";

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
