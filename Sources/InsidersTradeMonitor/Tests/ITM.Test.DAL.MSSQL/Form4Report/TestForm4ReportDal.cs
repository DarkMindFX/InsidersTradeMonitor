


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
    public class TestForm4ReportDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IForm4ReportDal dal = new Form4ReportDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Form4Report_GetAll_Success()
        {
            var dal = PrepareForm4ReportDal("DALInitParams");

            IList<Form4Report> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Form4Report\\000.GetDetails.Success")]
        public void Form4Report_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareForm4ReportDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            Form4Report entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(100004, entity.IssuerID);
            Assert.AreEqual(100004, entity.ReporterID);
            Assert.AreEqual(false, entity.IsOfficer);
            Assert.AreEqual(false, entity.IsDirector);
            Assert.AreEqual(false, entity.Is10PctHolder);
            Assert.AreEqual(false, entity.IsOther);
            Assert.AreEqual("OtherText 2b512676eb5b451eaaad7d001271987e", entity.OtherText);
            Assert.AreEqual("OfficerTitle 2b512676eb5b451eaaad7d001271987e", entity.OfficerTitle);
            Assert.AreEqual(DateTime.Parse("12/14/2024"), entity.Date);
        }

        [Test]
        public void Form4Report_GetDetails_InvalidId()
        {
            var paramID = Int64.MaxValue - 1;
            var dal = PrepareForm4ReportDal("DALInitParams");

            Form4Report entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Form4Report\\010.Delete.Success")]
        public void Form4Report_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareForm4ReportDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Form4Report_Delete_InvalidId()
        {
            var dal = PrepareForm4ReportDal("DALInitParams");
            var paramID = Int64.MaxValue - 1;

            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Form4Report\\020.Insert.Success")]
        public void Form4Report_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareForm4ReportDal("DALInitParams");

            var entity = new Form4Report();
            entity.IssuerID = 100013;
            entity.ReporterID = 100007;
            entity.IsOfficer = true;
            entity.IsDirector = true;
            entity.Is10PctHolder = true;
            entity.IsOther = true;
            entity.OtherText = "OtherText 4bb880efd3784e27ab2a1ac6b006e038";
            entity.OfficerTitle = "OfficerTitle 4bb880efd3784e27ab2a1ac6b006e038";
            entity.Date = DateTime.Parse("9/21/2019");

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(100013, entity.IssuerID);
            Assert.AreEqual(100007, entity.ReporterID);
            Assert.AreEqual(true, entity.IsOfficer);
            Assert.AreEqual(true, entity.IsDirector);
            Assert.AreEqual(true, entity.Is10PctHolder);
            Assert.AreEqual(true, entity.IsOther);
            Assert.AreEqual("OtherText 4bb880efd3784e27ab2a1ac6b006e038", entity.OtherText);
            Assert.AreEqual("OfficerTitle 4bb880efd3784e27ab2a1ac6b006e038", entity.OfficerTitle);
            Assert.AreEqual(DateTime.Parse("9/21/2019"), entity.Date);

        }

        [TestCase("Form4Report\\030.Update.Success")]
        public void Form4Report_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareForm4ReportDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            Form4Report entity = dal.Get(paramID);

            entity.IssuerID = 100008;
            entity.ReporterID = 100003;
            entity.IsOfficer = true;
            entity.IsDirector = true;
            entity.Is10PctHolder = true;
            entity.IsOther = true;
            entity.OtherText = "OtherText c268e54f65404093b67ecb0dfd2d949b";
            entity.OfficerTitle = "OfficerTitle c268e54f65404093b67ecb0dfd2d949b";
            entity.Date = DateTime.Parse("7/31/2022");

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(100008, entity.IssuerID);
            Assert.AreEqual(100003, entity.ReporterID);
            Assert.AreEqual(true, entity.IsOfficer);
            Assert.AreEqual(true, entity.IsDirector);
            Assert.AreEqual(true, entity.Is10PctHolder);
            Assert.AreEqual(true, entity.IsOther);
            Assert.AreEqual("OtherText c268e54f65404093b67ecb0dfd2d949b", entity.OtherText);
            Assert.AreEqual("OfficerTitle c268e54f65404093b67ecb0dfd2d949b", entity.OfficerTitle);
            Assert.AreEqual(DateTime.Parse("7/31/2022"), entity.Date);

        }

        [Test]
        public void Form4Report_Update_InvalidId()
        {
            var dal = PrepareForm4ReportDal("DALInitParams");

            var entity = new Form4Report();
            entity.IssuerID = 100008;
            entity.ReporterID = 100003;
            entity.IsOfficer = true;
            entity.IsDirector = true;
            entity.Is10PctHolder = true;
            entity.IsOther = true;
            entity.OtherText = "OtherText c268e54f65404093b67ecb0dfd2d949b";
            entity.OfficerTitle = "OfficerTitle c268e54f65404093b67ecb0dfd2d949b";
            entity.Date = DateTime.Parse("7/31/2022");

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


        protected IForm4ReportDal PrepareForm4ReportDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IForm4ReportDal dal = new Form4ReportDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
