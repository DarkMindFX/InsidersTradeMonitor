using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Test.Service.GraphQLTests
{
    public abstract class E2ETestBase
    {
        protected string LoadQuery(string filePath)
        {
            string result = null;
            string path = Path.Combine(TestBaseFolder, filePath);
            if(File.Exists(path))
            {
                result = File.ReadAllText(path);
            }

            return result;

        }

        protected string TestBaseFolder
        {
            get
            {
                return Path.Combine(TestContext.CurrentContext.TestDirectory, "..\\..\\..\\");
            }
        }

        protected IConfiguration GetConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json", optional: false, reloadOnChange: true)
                .Build();

            return config;
        }
    }
}
