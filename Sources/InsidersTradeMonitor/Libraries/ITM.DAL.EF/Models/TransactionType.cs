

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
                    DerivativeTransactions = new HashSet<DerivativeTransaction>();
                    NonDerivativeTransactions = new HashSet<NonDerivativeTransaction>();
                }

        
		[Key]
				public System.Int64? ID { get; set; }
				public System.String Code { get; set; }
				public System.String Description { get; set; }
			


        
                public virtual ICollection<DerivativeTransaction> DerivativeTransactions { get; set; }
                public virtual ICollection<NonDerivativeTransaction> NonDerivativeTransactions { get; set; }
            }
}