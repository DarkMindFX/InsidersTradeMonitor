


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

            Assert.AreEqual(100001, entity.IssuerID);
            Assert.AreEqual(100008, entity.ReporterID);
            Assert.AreEqual("ReportID 3f9cfc7cebd5406bb0dd8f1205276f64", entity.ReportID);
            Assert.AreEqual(false, entity.IsOfficer);
            Assert.AreEqual(false, entity.IsDirector);
            Assert.AreEqual(false, entity.Is10PctHolder);
            Assert.AreEqual(false, entity.IsOther);
            Assert.AreEqual("OtherText 3f9cfc7cebd5406bb0dd8f1205276f64", entity.OtherText);
            Assert.AreEqual("OfficerTitle 3f9cfc7cebd5406bb0dd8f1205276f64", entity.OfficerTitle);
            Assert.AreEqual(DateTime.Parse("10/6/2023"), entity.Date);
            Assert.AreEqual(DateTime.Parse("10/6/2023"), entity.DateSubmitted);
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
            entity.IssuerID = 100005;
            entity.ReporterID = 100006;
            entity.ReportID = "ReportID 9b939500b75b48f3be633fc3b3b1663e";
            entity.IsOfficer = false;
            entity.IsDirector = false;
            entity.Is10PctHolder = false;
            entity.IsOther = false;
            entity.OtherText = "OtherText 9b939500b75b48f3be633fc3b3b1663e";
            entity.OfficerTitle = "OfficerTitle 9b939500b75b48f3be633fc3b3b1663e";
            entity.Date = DateTime.Parse("2/23/2021");
            entity.DateSubmitted = DateTime.Parse("2/23/2021");

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(100005, entity.IssuerID);
            Assert.AreEqual(100006, entity.ReporterID);
            Assert.AreEqual("ReportID 9b939500b75b48f3be633fc3b3b1663e", entity.ReportID);
            Assert.AreEqual(false, entity.IsOfficer);
            Assert.AreEqual(false, entity.IsDirector);
            Assert.AreEqual(false, entity.Is10PctHolder);
            Assert.AreEqual(false, entity.IsOther);
            Assert.AreEqual("OtherText 9b939500b75b48f3be633fc3b3b1663e", entity.OtherText);
            Assert.AreEqual("OfficerTitle 9b939500b75b48f3be633fc3b3b1663e", entity.OfficerTitle);
            Assert.AreEqual(DateTime.Parse("2/23/2021"), entity.Date);
            Assert.AreEqual(DateTime.Parse("2/23/2021"), entity.DateSubmitted);

        }

        [TestCase("Form4Report\\030.Update.Success")]
        public void Form4Report_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareForm4ReportDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            Form4Report entity = dal.Get(paramID);

            entity.IssuerID = 100005;
            entity.ReporterID = 100006;
            entity.ReportID = "ReportID c843fad9b39b4eabba16844bc734cb6d";
            entity.IsOfficer = false;
            entity.IsDirector = false;
            entity.Is10PctHolder = false;
            entity.IsOther = false;
            entity.OtherText = "OtherText c843fad9b39b4eabba16844bc734cb6d";
            entity.OfficerTitle = "OfficerTitle c843fad9b39b4eabba16844bc734cb6d";
            entity.Date = DateTime.Parse("1/5/2024");
            entity.DateSubmitted = DateTime.Parse("1/5/2024");

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual(100005, entity.IssuerID);
            Assert.AreEqual(100006, entity.ReporterID);
            Assert.AreEqual("ReportID c843fad9b39b4eabba16844bc734cb6d", entity.ReportID);
            Assert.AreEqual(false, entity.IsOfficer);
            Assert.AreEqual(false, entity.IsDirector);
            Assert.AreEqual(false, entity.Is10PctHolder);
            Assert.AreEqual(false, entity.IsOther);
            Assert.AreEqual("OtherText c843fad9b39b4eabba16844bc734cb6d", entity.OtherText);
            Assert.AreEqual("OfficerTitle c843fad9b39b4eabba16844bc734cb6d", entity.OfficerTitle);
            Assert.AreEqual(DateTime.Parse("1/5/2024"), entity.Date);
            Assert.AreEqual(DateTime.Parse("1/5/2024"), entity.DateSubmitted);

        }

        [Test]
        public void Form4Report_Update_InvalidId()
        {
            var dal = PrepareForm4ReportDal("DALInitParams");

            var entity = new Form4Report();
            entity.IssuerID = 100005;
            entity.ReporterID = 100006;
            entity.ReportID = "ReportID c843fad9b39b4eabba16844bc734cb6d";
            entity.IsOfficer = false;
            entity.IsDirector = false;
            entity.Is10PctHolder = false;
            entity.IsOther = false;
            entity.OtherText = "OtherText c843fad9b39b4eabba16844bc734cb6d";
            entity.OfficerTitle = "OfficerTitle c843fad9b39b4eabba16844bc734cb6d";
            entity.Date = DateTime.Parse("1/5/2024");
            entity.DateSubmitted = DateTime.Parse("1/5/2024");

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
