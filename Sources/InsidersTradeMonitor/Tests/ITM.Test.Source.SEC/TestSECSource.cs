using ITM.Logging;
using ITM.Source.SEC;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using System;

namespace ITM.Test.Source.SEC
{
    public class TestSourceSEC : ITM.Test.Common.TestBase
    {
        class TestSourceSECSettings
        {
            public string AAPL_CIK { get; set; }
            public string AAPL_FORM4_20220419 { get; set; }
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void SECConfig_Init_Success()
        {
            var source = new SECSource();
            var initParams = source.CreateInitParams();

            initParams.Logger = new NullLogger();

            source.Init(initParams);
        }

        [Test]
        public void SECSource_GetFilingsList_Success()
        {
            var config = base.GetConfiguration().GetSection("TestSourceSECSettings").Get<TestSourceSECSettings>();

            var source = new SECSource();
            var initParams = source.CreateInitParams();

            initParams.Logger = new NullLogger();

            source.Init(initParams);

            var validateParams = source.CreateValidateParams();
            validateParams.CIK = config.AAPL_CIK;
            validateParams.UpdateFromDate = System.DateTime.Now.AddDays(-100);
            validateParams.UpdateToDate = System.DateTime.Now;

            var result = source.GetFilingsList(validateParams).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotEmpty(result.Filings);
            foreach(var f in result.Filings)
            {
                Assert.IsTrue(f.LastModified >= validateParams.UpdateFromDate);
                Assert.IsTrue(f.LastModified <= validateParams.UpdateToDate);
            }
        }

        [Test]
        public void SECSource_GetSubmissionsInfo_Success()
        {
            var config = base.GetConfiguration().GetSection("TestSourceSECSettings").Get<TestSourceSECSettings>();

            var source = new SECSource();
            var initParams = source.CreateInitParams();

            initParams.Logger = new NullLogger();

            source.Init(initParams);

            var validateParams = source.CreateSourceSubmissionsInfoParams();
            validateParams.CIK = config.AAPL_CIK;
            validateParams.Items.Add(new SECSourceItemInfo()
            {
                Name = config.AAPL_FORM4_20220419
            });

            var result = source.GetSubmissionsInfo(validateParams).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotEmpty(result.Submissions);
            Assert.IsTrue(result.Submissions[0].Type == "4");
        }



        [Test]
        public void SECSource_ExtractReports_Success()
        {
            var config = base.GetConfiguration().GetSection("TestSourceSECSettings").Get<TestSourceSECSettings>();

            var source = new SECSource();
            var initParams = source.CreateInitParams();

            initParams.Logger = new NullLogger();

            source.Init(initParams);

            var extractParams = source.CreateExtractParams();
            extractParams.CIK = config.AAPL_CIK;
            extractParams.Items.Add(new SECSourceItemInfo()
            {
                Name = config.AAPL_FORM4_20220419
            });

            var result = source.ExtractReports(extractParams).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotEmpty(result.Items);
            Assert.IsTrue(result.Items.Find(x => x.Name.Equals("wf-form4_165040749395911.xml")) != null);

        }
    }
}