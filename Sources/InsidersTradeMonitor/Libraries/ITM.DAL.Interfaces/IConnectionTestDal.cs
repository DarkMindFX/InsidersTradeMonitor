﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ITM.Interfaces
{
    public class ConnectionTestResult
    {
        public bool Success
        {
            get;
            set;
        }

        public IList<Exception> Errors
        {
            get;
            set;
        }
    }
    public interface IConnectionTestDal : IInitializable
    {
        ConnectionTestResult TestConnection();
    }
}
