﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Interfaces
{
    public interface ILoggerParams
    {
        Dictionary<string, object> Parameters
        {
            get;
            set;
        }
    }

    public interface ILogger : IDisposable
    {
        ILoggerParams CreateParams();

        void Init(ILoggerParams loggerParams);

        void Log(EErrorType type, string msg);

        void Log(Exception ex);


    }
}
