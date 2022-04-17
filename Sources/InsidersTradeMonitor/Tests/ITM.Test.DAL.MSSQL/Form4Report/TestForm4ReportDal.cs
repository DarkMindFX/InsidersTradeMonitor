


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
            
                          Assert.AreEqual(true, entity.IsOfficer);
                            Assert.AreEqual(true, entity.IsDirector);
                            Assert.AreEqual(true, entity.Is10PctHolder);
                            Assert.AreEqual(true, entity.IsOther);
                            Assert.AreEqual(true, entity.OtherText);
                            Assert.AreEqual("OfficerTitle 66020c17ad08400796074e7cca67685f", entity.OfficerTitle);
                            Assert.AreEqual(DateTime.Parse("10/12/2023 2:52:45 PM"), entity.Date);
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
                          entity.IsOfficer = false;              
                            entity.IsDirector = false;              
                            entity.Is10PctHolder = false;              
                            entity.IsOther = false;              
                            entity.OtherText = false;              
                            entity.OfficerTitle = "OfficerTitle 2fda46dee6534d19b72bcacca0704edb";
                            entity.Date = DateTime.Parse("5/30/2021 10:52:45 AM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(false, entity.IsOfficer);
                            Assert.AreEqual(false, entity.IsDirector);
                            Assert.AreEqual(false, entity.Is10PctHolder);
                            Assert.AreEqual(false, entity.IsOther);
                            Assert.AreEqual(false, entity.OtherText);
                            Assert.AreEqual("OfficerTitle 2fda46dee6534d19b72bcacca0704edb", entity.OfficerTitle);
                            Assert.AreEqual(DateTime.Parse("5/30/2021 10:52:45 AM"), entity.Date);
              
        }

        [TestCase("Form4Report\\030.Update.Success")]
        public void Form4Report_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareForm4ReportDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Form4Report entity = dal.Get(paramID);

                          entity.IsOfficer = false;              
                            entity.IsDirector = false;              
                            entity.Is10PctHolder = false;              
                            entity.IsOther = false;              
                            entity.OtherText = false;              
                            entity.OfficerTitle = "OfficerTitle cd23acfb4a4749f7b8b586d9d030083c";
                            entity.Date = DateTime.Parse("4/9/2024 8:39:45 PM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(false, entity.IsOfficer);
                            Assert.AreEqual(false, entity.IsDirector);
                            Assert.AreEqual(false, entity.Is10PctHolder);
                            Assert.AreEqual(false, entity.IsOther);
                            Assert.AreEqual(false, entity.OtherText);
                            Assert.AreEqual("OfficerTitle cd23acfb4a4749f7b8b586d9d030083c", entity.OfficerTitle);
                            Assert.AreEqual(DateTime.Parse("4/9/2024 8:39:45 PM"), entity.Date);
              
        }

        [Test]
        public void Form4Report_Update_InvalidId()
        {
            var dal = PrepareForm4ReportDal("DALInitParams");

            var entity = new Form4Report();
                          entity.IsOfficer = false;              
                            entity.IsDirector = false;              
                            entity.Is10PctHolder = false;              
                            entity.IsOther = false;              
                            entity.OtherText = false;              
                            entity.OfficerTitle = "OfficerTitle cd23acfb4a4749f7b8b586d9d030083c";
                            entity.Date = DateTime.Parse("4/9/2024 8:39:45 PM");
              
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
