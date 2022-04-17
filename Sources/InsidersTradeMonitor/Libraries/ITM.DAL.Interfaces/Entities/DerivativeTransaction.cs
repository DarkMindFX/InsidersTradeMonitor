


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.Interfaces.Entities
{
    public class DerivativeTransaction 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 Form4ReportID { get; set; }

				public System.String TitleOfDerivative { get; set; }

				public System.Decimal ConversionExercisePrice { get; set; }

				public System.DateTime TransactionDate { get; set; }

				public System.Int64 TransactionCodeID { get; set; }

				public System.Boolean EarlyVoluntarilyReport { get; set; }

				public System.Int64? SharesAmount { get; set; }

				public System.Decimal? DerivativeSecurityPrice { get; set; }

				public System.Int64? TransactionTypeID { get; set; }

				public System.DateTime? DateExercisable { get; set; }

				public System.DateTime? ExpirationDate { get; set; }

				public System.String UnderlyingTitle { get; set; }

				public System.Int64 UnderlyingSharesAmount { get; set; }

				public System.Int64 AmountFollowingReport { get; set; }

				public System.Int64 OwnershipTypeID { get; set; }

				public System.String NatureOfIndirectOwnership { get; set; }

				
    }
}
