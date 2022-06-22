

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Interfaces.Entities
{

    public enum EImportRunState
    {
        InProgress = 1,
        Success = 2,
        Fail = 3
    }

    public class ImportRunState
    {
        public System.Int64? ID { get; set; }

        public System.String Name { get; set; }


    }
}
