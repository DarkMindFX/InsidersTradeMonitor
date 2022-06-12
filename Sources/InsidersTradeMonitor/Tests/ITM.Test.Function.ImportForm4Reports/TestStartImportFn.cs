using NUnit.Framework;

namespace ITM.Test.Function.ImportForm4Reports
{
    public class TestStartImportFn
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var fn = new ITM.Function.V1.ImportForm4Reports.StartImportFn();
        }
    }
}