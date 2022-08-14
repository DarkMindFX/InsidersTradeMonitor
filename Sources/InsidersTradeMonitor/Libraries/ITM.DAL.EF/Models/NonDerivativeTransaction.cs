

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class NonDerivativeTransaction
    {
        public NonDerivativeTransaction()
        {
                }

        
		[Key]
				public System.Int64? ID { get; set; }
		
		[ForeignKey("FK_NonDerivativeTransaction_Form4Report")]
				public System.Int64 Form4ReportID { get; set; }
				public System.String TitleOfSecurity { get; set; }
				public System.DateTime TransactionDate { get; set; }
				public System.DateTime? DeemedExecDate { get; set; }
		
		[ForeignKey("FK_NonDerivativeTransaction_TransactionCode")]
				public System.Int64? TransactionCodeID { get; set; }
				public System.Boolean EarlyVoluntarilyReport { get; set; }
				public System.Int64? SharesAmount { get; set; }
		
		[ForeignKey("FK_NonDerivativeTransaction_TransactionType")]
				public System.Int64? TransactionTypeID { get; set; }
				public System.Decimal Price { get; set; }
				public System.Int64 AmountFollowingReport { get; set; }
		
		[ForeignKey("FK_NonDerivativeTransaction_OwnershipType")]
				public System.Int64? OwnershipTypeID { get; set; }
				public System.String NatureOfIndirectOwnership { get; set; }
			


                public virtual Form4Report Form4Report { get; set; }
                public virtual TransactionCode TransactionCode { get; set; }
                public virtual TransactionType TransactionType { get; set; }
                public virtual OwnershipType OwnershipType { get; set; }
        
            }
}