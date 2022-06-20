

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Interfaces.Entities
{
    public class ImportRunForm4Report 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 ImportRunID { get; set; }

				public System.Int64 Form4ReportID { get; set; }

				public System.DateTime TimeStarted { get; set; }

				public System.DateTime? TimeCompleted { get; set; }

				
    }
}
