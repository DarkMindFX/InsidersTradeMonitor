using ITM.Interfaces;
using System.Collections.Generic;


namespace ITM.DAL.EF.Dals
{
    public class InitParamsImpl : IInitParams
    {
        public InitParamsImpl()
        {
            Parameters = new Dictionary<string, string>();
            Parameters["ConnectionString"] = string.Empty;
        }

        public Dictionary<string, string> Parameters
        {
            get;
            set;
        }
    }
}
