


using ITM.DAL.MSSQL;
using ITM.Interfaces;
using ITM.Interfaces.Entities;
using ITM.Test.Common;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Test.ITM.DAL.MSSQL
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

            Assert.That(entity.IssuerID, Is.EqualTo(100001));
            Assert.That(entity.ReporterID, Is.EqualTo(100008));
            Assert.That(entity.ReportID, Is.EqualTo("ReportID 3f9cfc7cebd5406bb0dd8f1205276f64"));
            Assert.That(entity.IsOfficer, Is.EqualTo(false));
            Assert.That(entity.IsDirector, Is.EqualTo(false));
            Assert.That(entity.Is10PctHolder, Is.EqualTo(false));
            Assert.That(entity.IsOther, Is.EqualTo(false));
            Assert.That(entity.OtherText, Is.EqualTo("OtherText 3f9cfc7cebd5406bb0dd8f1205276f64"));
            Assert.That(entity.OfficerTitle, Is.EqualTo("OfficerTitle 3f9cfc7cebd5406bb0dd8f1205276f64"));
            Assert.That(entity.Date, Is.EqualTo(DateTime.Parse("10/6/2023")));
            Assert.That(entity.DateSubmitted, Is.EqualTo(DateTime.Parse("10/6/2023")));
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

            Assert.That(entity.IssuerID, Is.EqualTo(100005));
            Assert.That(entity.ReporterID, Is.EqualTo(100006));
            Assert.That(entity.ReportID, Is.EqualTo("ReportID 9b939500b75b48f3be633fc3b3b1663e"));
            Assert.That(entity.IsOfficer, Is.EqualTo(false));
            Assert.That(entity.IsDirector, Is.EqualTo(false));
            Assert.That(entity.Is10PctHolder, Is.EqualTo(false));
            Assert.That(entity.IsOther, Is.EqualTo(false));
            Assert.That(entity.OtherText, Is.EqualTo("OtherText 9b939500b75b48f3be633fc3b3b1663e"));
            Assert.That(entity.OfficerTitle, Is.EqualTo("OfficerTitle 9b939500b75b48f3be633fc3b3b1663e"));
            Assert.That(entity.Date, Is.EqualTo(DateTime.Parse("2/23/2021")));
            Assert.That(entity.DateSubmitted, Is.EqualTo(DateTime.Parse("2/23/2021")));

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

            Assert.That(entity.IssuerID, Is.EqualTo(100005));
            Assert.That(entity.ReporterID, Is.EqualTo(100006));
            Assert.That(entity.ReportID, Is.EqualTo("ReportID c843fad9b39b4eabba16844bc734cb6d"));
            Assert.That(entity.IsOfficer, Is.EqualTo(false));
            Assert.That(entity.IsDirector, Is.EqualTo(false));
            Assert.That(entity.Is10PctHolder, Is.EqualTo(false));
            Assert.That(entity.IsOther, Is.EqualTo(false));
            Assert.That(entity.OtherText, Is.EqualTo("OtherText c843fad9b39b4eabba16844bc734cb6d"));
            Assert.That(entity.OfficerTitle, Is.EqualTo("OfficerTitle c843fad9b39b4eabba16844bc734cb6d"));
            Assert.That(entity.Date, Is.EqualTo(DateTime.Parse("1/5/2024")));
            Assert.That(entity.DateSubmitted, Is.EqualTo(DateTime.Parse("1/5/2024")));

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
