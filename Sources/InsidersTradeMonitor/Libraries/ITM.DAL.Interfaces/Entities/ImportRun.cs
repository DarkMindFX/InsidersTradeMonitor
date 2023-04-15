

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Interfaces.Entities
{
    public class ImportRun
    {
        public System.Int64? ID { get; set; }

        public System.DateTime TimeStart { get; set; }

        public System.DateTime? TimeEnd { get; set; }

        public System.String RequestJson { get; set; }

        public System.Int64 StateID { get; set; }


    }
}
