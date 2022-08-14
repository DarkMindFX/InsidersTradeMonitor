

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Interfaces.Entities
{
    public class Entity 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 EntityTypeID { get; set; }

				public System.Int32 CIK { get; set; }

				public System.String Name { get; set; }

				public System.String TradingSymbol { get; set; }

				public System.Boolean IsMonitored { get; set; }

				
    }
}
