using ITM.DTO;
using ITM.Function.ImportForm4Reports;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ITM.Test.Function.ImportForm4Reports
{
    public class TestStartImportFn : ImporterTestBase
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void ImportOneDay_Success()
        {
            IList<long> ids = null;
            try
            {
                var fn = new ITM.Function.V1.ImportForm4Reports.StartImportFn(base.PrepareForm4DalWrapper(),
                                                                              base.PrepareImportRunDalFacade());

                var payload = new RpcStartImport()
                {
                    CIK = "320193", // AAPL
                    DateFrom = DateTime.Parse("2022/04/19"),
                    DateTo = DateTime.Parse("2022/04/20")
                };

                var message = new MessageBase()
                {
                    Name = "StartImport",
                    Payload = JsonSerializer.Serialize<RpcStartImport>(payload)
                };

                fn.Run(JsonSerializer.Serialize<MessageBase>(message));

                ids = fn.ReportsIDs;

                Assert.NotNull(ids);
                Assert.IsTrue(ids.Count == 2);

            }
            finally
            {
                base.CleanUpReports(ids);
            }

        }

        [Test]
        public void ImportSevaralDays_Success()
        {
            IList<long> ids = null;
            try
            {
                var fn = new ITM.Function.V1.ImportForm4Reports.StartImportFn(base.PrepareForm4DalWrapper(),
                                                                              base.PrepareImportRunDalFacade());

                var payload = new RpcStartImport()
                {
                    CIK = "320193", // AAPL
                    DateFrom = DateTime.Parse("2022/01/01"),
                    DateTo = DateTime.Parse("2022/06/30")
                };

                var message = new MessageBase()
                {
                    Name = "StartImport",
                    Payload = JsonSerializer.Serialize<RpcStartImport>(payload)
                };

                fn.Run(JsonSerializer.Serialize<MessageBase>(message));

                ids = fn.ReportsIDs;

                Assert.NotNull(ids);
                Assert.IsTrue(ids.Count == 6);

            }
            finally
            {
                base.CleanUpReports(ids);
            }

        }

        [Test]
        public void ImportSevaralDays_WrongMessageName()
        {
            IList<long> ids = null;
            try
            {
                var fn = new ITM.Function.V1.ImportForm4Reports.StartImportFn(base.PrepareForm4DalWrapper(),
                                                                              base.PrepareImportRunDalFacade());

                var payload = new RpcStartImport()
                {
                    CIK = "320193", // AAPL
                    DateFrom = DateTime.Parse("2022/03/20"),
                    DateTo = DateTime.Parse("2022/04/20")
                };

                var message = new MessageBase()
                {
                    Name = "@Incorrect message name@",
                    Payload = JsonSerializer.Serialize<RpcStartImport>(payload)
                };

                fn.Run(JsonSerializer.Serialize<MessageBase>(message));

                ids = fn.ReportsIDs;

                Assert.IsNull(ids);

            }
            finally
            {
                base.CleanUpReports(ids);
            }

        }

        [Test]
        public void ImportOneDay_NoReports()
        {
            IList<long> ids = null;
            try
            {
                var fn = new ITM.Function.V1.ImportForm4Reports.StartImportFn(base.PrepareForm4DalWrapper(),
                                                                              base.PrepareImportRunDalFacade());

                var payload = new RpcStartImport()
                {
                    CIK = "320193", // AAPL
                    DateFrom = DateTime.Parse("2022/04/23"),
                    DateTo = DateTime.Parse("2022/04/24")
                };

                var message = new MessageBase()
                {
                    Name = "StartImport",
                    Payload = JsonSerializer.Serialize<RpcStartImport>(payload)
                };

                fn.Run(JsonSerializer.Serialize<MessageBase>(message));

                ids = fn.ReportsIDs;

                Assert.NotNull(ids);
                Assert.IsTrue(ids.Count == 0);

            }
            finally
            {
                base.CleanUpReports(ids);
            }

        }
        
    }
}