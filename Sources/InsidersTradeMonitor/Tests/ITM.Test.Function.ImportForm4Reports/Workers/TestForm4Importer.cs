using ITM.Function.ImportForm4Reports;
using ITM.Function.ImportForm4Reports.Workers;
using ITM.Interfaces;
using ITM.Interfaces.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ITM.Test.Function.ImportForm4Reports.Workers
{
    public class TestForm4Importer : ImporterTestBase
    {
        

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ImportOneDay_Success()
        {
            IList<long> ids = null;
            ISourceInitParams sourceInitParams = null;
            try
            {
                var parser = new ITM.Parser.Form4.Form4Parser();

                var source = new ITM.Source.SEC.SECSource();
                sourceInitParams = source.CreateInitParams();
                sourceInitParams.Logger = new ITM.Logging.NullLogger();

                source.Init(sourceInitParams);

                var form4DalWrapper = PrepareForm4DalWrapper();

                var impParams = new Form4ImporterParams()
                {
                    CIK = "320193",
                    DateFrom = DateTime.Parse("2022/04/19"),
                    DateTo = DateTime.Parse("2022/04/20"),
                    FilingParser = parser,
                    Source = source,
                    Form4DalWrappwer = form4DalWrapper
                };

                var importer = new Form4Importer(impParams);                

                ids = importer.Import();

                Assert.NotNull(ids);
                Assert.IsTrue(ids.Count == 2);
            }
            finally
            {
                base.CleanUpReports(ids);
            }
        }


        
    }
}
