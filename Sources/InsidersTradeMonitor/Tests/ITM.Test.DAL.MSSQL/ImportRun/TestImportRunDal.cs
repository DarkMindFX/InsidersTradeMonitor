


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
    public class TestImportRunDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IImportRunDal dal = new ImportRunDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ImportRun_GetAll_Success()
        {
            var dal = PrepareImportRunDal("DALInitParams");

            IList<ImportRun> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ImportRun\\000.GetDetails.Success")]
        public void ImportRun_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImportRunDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            ImportRun entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.TimeStart, Is.EqualTo(DateTime.Parse("10/9/2019 6:02:20 PM")));
            Assert.That(entity.TimeEnd, Is.EqualTo(DateTime.Parse("10/9/2019 6:02:20 PM")));
            Assert.That(entity.RequestJson, Is.EqualTo("RequestJson 24be0457da26426abb2365356302670b"));
            Assert.That(entity.StateID, Is.EqualTo(1));
        }

        [Test]
        public void ImportRun_GetDetails_InvalidId()
        {
            var paramID = Int64.MaxValue - 1;
            var dal = PrepareImportRunDal("DALInitParams");

            ImportRun entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("ImportRun\\010.Delete.Success")]
        public void ImportRun_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImportRunDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ImportRun_Delete_InvalidId()
        {
            var dal = PrepareImportRunDal("DALInitParams");
            var paramID = Int64.MaxValue - 1;

            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("ImportRun\\020.Insert.Success")]
        public void ImportRun_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareImportRunDal("DALInitParams");

            var entity = new ImportRun();
            entity.TimeStart = DateTime.Parse("10/9/2019 6:02:20 PM");
            entity.TimeEnd = DateTime.Parse("10/9/2019 6:02:20 PM");
            entity.RequestJson = "RequestJson a387175cb12b4b619f24b3ab62e5c6b7";
            entity.StateID = 1;

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.TimeStart, Is.EqualTo(DateTime.Parse("10/9/2019 6:02:20 PM")));
            Assert.That(entity.TimeEnd, Is.EqualTo(DateTime.Parse("10/9/2019 6:02:20 PM")));
            Assert.That(entity.RequestJson, Is.EqualTo("RequestJson a387175cb12b4b619f24b3ab62e5c6b7"));
            Assert.That(entity.StateID, Is.EqualTo(1));

        }

        [TestCase("ImportRun\\030.Update.Success")]
        public void ImportRun_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImportRunDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            ImportRun entity = dal.Get(paramID);

            entity.TimeStart = DateTime.Parse("1/7/2020 4:15:20 AM");
            entity.TimeEnd = DateTime.Parse("1/7/2020 4:15:20 AM");
            entity.RequestJson = "RequestJson 7624bb2396c14384b5413b858b376ce6";
            entity.StateID = 3;

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.That(entity.TimeStart, Is.EqualTo(DateTime.Parse("1/7/2020 4:15:20 AM")));
            Assert.That(entity.TimeEnd, Is.EqualTo(DateTime.Parse("1/7/2020 4:15:20 AM")));
            Assert.That(entity.RequestJson, Is.EqualTo("RequestJson 7624bb2396c14384b5413b858b376ce6"));
            Assert.That(entity.StateID, Is.EqualTo(3));

        }

        [Test]
        public void ImportRun_Update_InvalidId()
        {
            var dal = PrepareImportRunDal("DALInitParams");

            var entity = new ImportRun();
            entity.TimeStart = DateTime.Parse("1/7/2020 4:15:20 AM");
            entity.TimeEnd = DateTime.Parse("1/7/2020 4:15:20 AM");
            entity.RequestJson = "RequestJson 7624bb2396c14384b5413b858b376ce6";
            entity.StateID = 3;

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


        protected IImportRunDal PrepareImportRunDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IImportRunDal dal = new ImportRunDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
