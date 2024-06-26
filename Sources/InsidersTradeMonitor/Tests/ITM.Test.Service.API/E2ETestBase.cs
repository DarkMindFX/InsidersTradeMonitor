﻿
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Test.E2E.API
{
    public abstract class E2ETestBase
    {
        public class TestParams
        {
            public TestParams()
            {
                Settings = new Dictionary<string, object>();
            }

            public Dictionary<string, object> Settings
            {
                get;
                set;
            }
        }

        protected TestParams _testParams;      
        protected readonly TestServer _testServer;
       
        public E2ETestBase(TestServer testServer)
        {
            this._testServer = testServer;
        }

        protected ITM.DTO.LoginResponse Login(string login, string password)
        {
            using (var client = _testServer.CreateClient())
            {
                var dtoLogin = new ITM.DTO.LoginRequest()
                {
                    Login = login,
                    Password = password
                };
                var content = CreateContentJson(dtoLogin);

                client.DefaultRequestHeaders.Host = "localhost:8082";

                var respLogin = client.PostAsync($"/api/v1/users/login", content);

                var dtoResponse = ExtractContentJson<ITM.DTO.LoginResponse>(respLogin.Result.Content);

                return dtoResponse;
            }
        }

        protected HttpContent CreateContentJson(object data)
        {
            var content = JsonSerializer.Serialize(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new StringContent(content, Encoding.UTF8, "application/json");
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        protected TResult ExtractContentJson<TResult>(HttpContent content)
        {
            var bytes = content.ReadAsByteArrayAsync().Result;
            var sContent = System.Text.Encoding.UTF8.GetString(bytes);
            TResult result = JsonSerializer.Deserialize<TResult>(sContent);

            return result;
        }

        protected TestParams GetTestParams(string name)
        {
            TestParams testParams = new TestParams();

            var config = GetConfiguration();

            testParams.Settings = config.GetSection(name).GetChildren().ToDictionary(x => x.Key, x => (object)x.Value);

            return testParams;
        }

        protected IConfiguration GetConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return config;
        }
    }
}
