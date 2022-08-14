

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class ImportRunForm4Report
    {
        public ImportRunForm4Report()
        {
                }

        
		[Key]
				public System.Int64? ID { get; set; }
		
		[ForeignKey("FK_ImportRunForm4Report_ImportRun")]
				public System.Int64 ImportRunID { get; set; }
		
		[ForeignKey("FK_ImportRunForm4Report_Form4Report")]
				public System.Int64 Form4ReportID { get; set; }
				public System.DateTime TimeStarted { get; set; }
				public System.DateTime? TimeCompleted { get; set; }
			


                public virtual ImportRun ImportRun { get; set; }
                public virtual Form4Report Form4Report { get; set; }
        
            }
}