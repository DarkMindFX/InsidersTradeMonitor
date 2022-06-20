


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
    public class TestImportRunForm4ReportDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IImportRunForm4ReportDal dal = new ImportRunForm4ReportDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ImportRunForm4Report_GetAll_Success()
        {
            var dal = PrepareImportRunForm4ReportDal("DALInitParams");

            IList<ImportRunForm4Report> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ImportRunForm4Report\\000.GetDetails.Success")]
        public void ImportRunForm4Report_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImportRunForm4ReportDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ImportRunForm4Report entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100004, entity.ImportRunID);
                            Assert.AreEqual(100023, entity.Form4ReportID);
                            Assert.AreEqual(DateTime.Parse("7/4/2020 10:03:20 AM"), entity.TimeStarted);
                            Assert.AreEqual(DateTime.Parse("7/4/2020 10:03:20 AM"), entity.TimeCompleted);
                      }

        [Test]
        public void ImportRunForm4Report_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareImportRunForm4ReportDal("DALInitParams");

            ImportRunForm4Report entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("ImportRunForm4Report\\010.Delete.Success")]
        public void ImportRunForm4Report_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImportRunForm4ReportDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ImportRunForm4Report_Delete_InvalidId()
        {
            var dal = PrepareImportRunForm4ReportDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("ImportRunForm4Report\\020.Insert.Success")]
        public void ImportRunForm4Report_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareImportRunForm4ReportDal("DALInitParams");

            var entity = new ImportRunForm4Report();
                          entity.ImportRunID = 100005;
                            entity.Form4ReportID = 100014;
                            entity.TimeStarted = DateTime.Parse("5/15/2023 7:50:20 PM");
                            entity.TimeCompleted = DateTime.Parse("5/15/2023 7:50:20 PM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100005, entity.ImportRunID);
                            Assert.AreEqual(100014, entity.Form4ReportID);
                            Assert.AreEqual(DateTime.Parse("5/15/2023 7:50:20 PM"), entity.TimeStarted);
                            Assert.AreEqual(DateTime.Parse("5/15/2023 7:50:20 PM"), entity.TimeCompleted);
              
        }

        [TestCase("ImportRunForm4Report\\030.Update.Success")]
        public void ImportRunForm4Report_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareImportRunForm4ReportDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ImportRunForm4Report entity = dal.Get(paramID);

                          entity.ImportRunID = 100009;
                            entity.Form4ReportID = 100030;
                            entity.TimeStarted = DateTime.Parse("5/15/2023 7:50:20 PM");
                            entity.TimeCompleted = DateTime.Parse("5/15/2023 7:50:20 PM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100009, entity.ImportRunID);
                            Assert.AreEqual(100030, entity.Form4ReportID);
                            Assert.AreEqual(DateTime.Parse("5/15/2023 7:50:20 PM"), entity.TimeStarted);
                            Assert.AreEqual(DateTime.Parse("5/15/2023 7:50:20 PM"), entity.TimeCompleted);
              
        }

        [Test]
        public void ImportRunForm4Report_Update_InvalidId()
        {
            var dal = PrepareImportRunForm4ReportDal("DALInitParams");

            var entity = new ImportRunForm4Report();
                          entity.ImportRunID = 100009;
                            entity.Form4ReportID = 100030;
                            entity.TimeStarted = DateTime.Parse("5/15/2023 7:50:20 PM");
                            entity.TimeCompleted = DateTime.Parse("5/15/2023 7:50:20 PM");
              
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


        protected IImportRunForm4ReportDal PrepareImportRunForm4ReportDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IImportRunForm4ReportDal dal = new ImportRunForm4ReportDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
