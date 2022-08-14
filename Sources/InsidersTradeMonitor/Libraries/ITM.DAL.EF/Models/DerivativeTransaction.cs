

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class DerivativeTransaction
    {
        public DerivativeTransaction()
        {
                }

        
		[Key]
				public System.Int64? ID { get; set; }
		
		[ForeignKey("FK_DerivativeTransaction_Form4Report")]
				public System.Int64 Form4ReportID { get; set; }
				public System.String TitleOfDerivative { get; set; }
				public System.Decimal ConversionExercisePrice { get; set; }
				public System.DateTime TransactionDate { get; set; }
		
		[ForeignKey("FK_DerivativeTransaction_TransactionCode")]
				public System.Int64 TransactionCodeID { get; set; }
				public System.Boolean EarlyVoluntarilyReport { get; set; }
				public System.Int64? SharesAmount { get; set; }
				public System.Decimal? DerivativeSecurityPrice { get; set; }
		
		[ForeignKey("FK_DerivativeTransaction_TransactionType")]
				public System.Int64? TransactionTypeID { get; set; }
				public System.DateTime? DateExercisable { get; set; }
				public System.DateTime? ExpirationDate { get; set; }
				public System.String UnderlyingTitle { get; set; }
				public System.Int64 UnderlyingSharesAmount { get; set; }
				public System.Int64 AmountFollowingReport { get; set; }
		
		[ForeignKey("FK_DerivativeTransaction_OwnershipType")]
				public System.Int64 OwnershipTypeID { get; set; }
				public System.String NatureOfIndirectOwnership { get; set; }
			


                public virtual Form4Report Form4Report { get; set; }
                public virtual TransactionCode TransactionCode { get; set; }
                public virtual TransactionType TransactionType { get; set; }
                public virtual OwnershipType OwnershipType { get; set; }
        
            }
}