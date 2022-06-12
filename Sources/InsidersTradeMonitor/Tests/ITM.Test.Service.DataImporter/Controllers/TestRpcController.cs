using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITM.Test.Service.DataImporter.Controllers
{
    public class TestRpcController :  E2ETestBase, IClassFixture<WebApplicationFactory<ITM.Service.DataImporter.Startup>>
    {
        public TestRpcController(WebApplicationFactory<ITM.Service.DataImporter.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }
    }
}
