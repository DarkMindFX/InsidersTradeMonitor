

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class Form4Report
    {
        public Form4Report()
        {
                    DerivativeTransactions = new HashSet<DerivativeTransaction>();
                    ImportRunForm4Reports = new HashSet<ImportRunForm4Report>();
                    NonDerivativeTransactions = new HashSet<NonDerivativeTransaction>();
                }

        
		[Key]
				public System.Int64? ID { get; set; }
		
		[ForeignKey("FK_Form4Report_Issuer")]
				public System.Int64 IssuerID { get; set; }
		
		[ForeignKey("FK_Form4Report_Reporter")]
				public System.Int64 ReporterID { get; set; }
				public System.String ReportID { get; set; }
				public System.Boolean IsOfficer { get; set; }
				public System.Boolean IsDirector { get; set; }
				public System.Boolean Is10PctHolder { get; set; }
				public System.Boolean IsOther { get; set; }
				public System.String OtherText { get; set; }
				public System.String OfficerTitle { get; set; }
				public System.DateTime Date { get; set; }
				public System.DateTime DateSubmitted { get; set; }
			


                public virtual Entity Issuer { get; set; }
                public virtual Entity Reporter { get; set; }
        
                public virtual ICollection<DerivativeTransaction> DerivativeTransactions { get; set; }
                public virtual ICollection<ImportRunForm4Report> ImportRunForm4Reports { get; set; }
                public virtual ICollection<NonDerivativeTransaction> NonDerivativeTransactions { get; set; }
            }
}