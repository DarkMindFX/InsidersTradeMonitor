using ITM.Parser.Form4;
using ITM.Test.Common;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Configuration;
using System.IO;

namespace ITM.Test.Form4Parser
{
    public class TestForm4Parser : TestBase
    {
        class TestAAPLForm4Params
        {
            public string AAPL_Form4_NonDerivs_Only { get; set; }

            public string AAPL_Form4_NonDerivs_Derivs { get; set; }
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ParseAAPLForm4NonDerivesOnly_Success()
        {
            IConfiguration config = GetConfiguration();
            var testParams = config.GetSection("TestForm4Params").Get<TestAAPLForm4Params>();

            var filePath = Path.Combine(base.TestBaseFolder, testParams.AAPL_Form4_NonDerivs_Only);
            var stream = new FileStream(filePath, FileMode.Open);

            var parser = new ITM.Parser.Form4.Form4Parser();
            var parserParams = new ITM.Parser.Form4.Form4ParserParams()
            {
                FileContent = stream
            };

            var result = parser.Parse(parserParams);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Statement);
            Assert.IsTrue(result.Statement is Form4Report);

            Assert.IsNotNull((result.Statement as Form4Report).NonDerivativeTransactions);
            Assert.IsNotEmpty((result.Statement as Form4Report).NonDerivativeTransactions);
            Assert.AreEqual(5, (result.Statement as Form4Report).NonDerivativeTransactions.Count);

            Assert.IsNull((result.Statement as Form4Report).DerivativeTransactions);

        }

        [Test]
        public void ParseAAPLForm4NonDerives_Derivs_Success()
        {
            IConfiguration config = GetConfiguration();
            var testParams = config.GetSection("TestForm4Params").Get<TestAAPLForm4Params>();

            var filePath = Path.Combine(base.TestBaseFolder, testParams.AAPL_Form4_NonDerivs_Derivs);
            var stream = new FileStream(filePath, FileMode.Open);

            var parser = new ITM.Parser.Form4.Form4Parser();
            var parserParams = new ITM.Parser.Form4.Form4ParserParams()
            {
                FileContent = stream
            };

            var result = parser.Parse(parserParams);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Statement);
            Assert.IsTrue(result.Statement is Form4Report);

            Assert.IsNotNull((result.Statement as Form4Report).NonDerivativeTransactions);
            Assert.IsNotEmpty((result.Statement as Form4Report).NonDerivativeTransactions);
            Assert.AreEqual(5, (result.Statement as Form4Report).NonDerivativeTransactions.Count);

            Assert.IsNotEmpty((result.Statement as Form4Report).DerivativeTransactions);
            Assert.AreEqual(1, (result.Statement as Form4Report).DerivativeTransactions.Count);

        }

        #region Support methods
        #endregion
    }
}