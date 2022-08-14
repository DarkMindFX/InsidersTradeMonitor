

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class EntityType
    {
        public EntityType()
        {
                    Entities = new HashSet<Entity>();
                }

        
		[Key]
				public System.Int64? ID { get; set; }
				public System.String TypeName { get; set; }
			


        
                public virtual ICollection<Entity> Entities { get; set; }
            }
}