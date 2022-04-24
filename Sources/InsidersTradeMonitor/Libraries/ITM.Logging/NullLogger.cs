using ITM.Interfaces;
using System.ComponentModel.Composition;

namespace ITM.Logging
{
    public class NullLoggerParams : ILoggerParams
    {
        public NullLoggerParams()
        {
            Parameters = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Parameters
        {
            get;
            set;
        }
    }

    [Export("NullLogger", typeof(ILogger))]
    public class NullLogger : ILogger
    {
        public ILoggerParams CreateParams()
        {
            return new NullLoggerParams();
        }

        public void Dispose()
        {
            
        }

        public void Init(ILoggerParams loggerParams)
        {
            NullLoggerParams parameters = loggerParams as NullLoggerParams;
        }

        public void Log(Exception ex)
        {
            
        }

        public void Log(EErrorType type, string msg)
        {
            
        }
    }
}
