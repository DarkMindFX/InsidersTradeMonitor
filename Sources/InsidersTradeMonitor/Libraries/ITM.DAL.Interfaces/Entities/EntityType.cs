


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Interfaces.Entities
{
    public enum EEntityType
    {
        Company = 1,
        Person = 2
    }

    public class EntityType
    {
        public System.Int64? ID { get; set; }

        public System.String TypeName { get; set; }


    }
}
