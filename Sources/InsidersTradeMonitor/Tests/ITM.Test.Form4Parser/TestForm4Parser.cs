using ITM.Test.Common;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Configuration;
using System.IO;

namespace ITM.Test.Form4Parser
{
    public class TestAAPLForm4 : TestBase
    {
        class TestAAPLForm4Params
        {
            public string Form4_NonDerivs_Only { get; set; }

            public string Form4_NonDerivs_Derivs { get; set; }
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ParseAAPLForm4NonDerivesOnly_Success()
        {
            IConfiguration config = GetConfiguration();
            var testParams = config.GetSection("TestAAPLForm4Params").Get<TestAAPLForm4Params>();

            var filePath = ConfigurationPath.Combine(base.TestBaseFolder, testParams.Form4_NonDerivs_Only);
            var stream = new FileStream(filePath, FileMode.Open);

            var parser = new ITM.Parser.Form4.Form4Parser();
            var parserParams = new ITM.Parser.Form4.Form4ParserParams()
            {
                FileContent = stream
            };

            var result = parser.Parse(parserParams);
        }

        #region Support methods
        #endregion
    }
}