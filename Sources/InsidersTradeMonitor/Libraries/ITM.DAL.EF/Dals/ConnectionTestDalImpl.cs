
using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class ConnectionTestDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IConnectionTestDal))]
    public class ConnectionTestDalImpl : IConnectionTestDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new ConnectionTestDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ConnectionTestResult TestConnection()
        {
            var result = new ConnectionTestResult();
            result.Errors = new List<Exception>();

            try
            {
                var entityTypes = from p in dbContext.EntityTypes
                                  select p;

                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Errors.Add(ex);
            }

            return result;
        }
    }
}
