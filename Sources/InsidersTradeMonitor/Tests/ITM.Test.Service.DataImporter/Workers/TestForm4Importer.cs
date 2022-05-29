using ITM.Interfaces;
using ITM.Service.DataImporter.Helpers;
using ITM.Service.DataImporter.Workers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Test.Service.DataImporter.Workers
{
    public class TestForm4Importer : ITM.Test.Common.TestBase
    {
        public class For4ImporterTestWrapper : Form4Importer
        {
            public For4ImporterTestWrapper(Form4ImporterParams impParams) : base(impParams)
            {    
            }

            public void ImporterThread()
            {
                base.ImporterThread();
            }
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ImportOneDay_Success()
        {
            var parser = new ITM.Parser.Form4.Form4Parser();

            var source = new ITM.Source.SEC.SECSource();
            var sourceInitParams = source.CreateInitParams();
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
                
            };

            var wrapper = new For4ImporterTestWrapper(impParams);
            wrapper.ImporterThread();
        }

        #region Support methods
        private IForm4DalWrapper PrepareForm4DalWrapper()
        {
            IForm4DalWrapper wrapper = null;

            return wrapper;
        }

        private TDal CreateDal<TDal, TEntity>(string configParamName) where TDal : IDalBase<TEntity>, new()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            TDal dal = new TDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        #endregion
    }
}
