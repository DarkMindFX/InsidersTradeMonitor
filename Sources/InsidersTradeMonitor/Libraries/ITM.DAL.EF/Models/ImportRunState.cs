

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class ImportRunState
    {
        public ImportRunState()
        {
                    ImportRuns = new HashSet<ImportRun>();
                }

        
		[Key]
				public System.Int64? ID { get; set; }
				public System.String Name { get; set; }
			


        
                public virtual ICollection<ImportRun> ImportRuns { get; set; }
            }
}