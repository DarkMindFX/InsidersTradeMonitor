

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class ImportRun
    {
        public ImportRun()
        {
                    ImportRunForm4Reports = new HashSet<ImportRunForm4Report>();
                }

        
		[Key]
				public System.Int64? ID { get; set; }
				public System.DateTime TimeStart { get; set; }
				public System.DateTime? TimeEnd { get; set; }
				public System.String RequestJson { get; set; }
		
		[ForeignKey("FK_ImportRun_ImportRunState")]
				public System.Int64 StateID { get; set; }
			


                public virtual ImportRunState State { get; set; }
        
                public virtual ICollection<ImportRunForm4Report> ImportRunForm4Reports { get; set; }
            }
}