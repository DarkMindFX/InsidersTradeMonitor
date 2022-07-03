using ITM.DAL.MSSQL;
using ITM.Interfaces;
using ITM.Test.Common;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.PPT.DAL.MSSQL
{
    public class TestForm4ReportExtDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IForm4ReportExtDal dal = new Form4ReportExtDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Form4ReportExt_GetComplete_Success()
        {
            var dal = PrepareForm4ReportExtDal("DALInitParams");

            long reportID = 100001;

            var report = dal.GetComplete(reportID);

            Assert.IsNotNull(report);
            Assert.IsNotNull(report.ReportDetails);
        }

        #region Support methods
        protected IForm4ReportExtDal PrepareForm4ReportExtDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IForm4ReportExtDal dal = new Form4ReportExtDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
