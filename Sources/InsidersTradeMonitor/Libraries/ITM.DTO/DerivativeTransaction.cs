

using PPT.DTO;
using System.Text.Json.Serialization;

namespace ITM.DTO
{
    public class DerivativeTransaction : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Form4ReportID")]
		public System.Int64 Form4ReportID { get; set; }

				[JsonPropertyName("TitleOfDerivative")]
		public System.String TitleOfDerivative { get; set; }

				[JsonPropertyName("ConversionExercisePrice")]
		public System.Decimal ConversionExercisePrice { get; set; }

				[JsonPropertyName("TransactionDate")]
		public System.DateTime TransactionDate { get; set; }

				[JsonPropertyName("TransactionCodeID")]
		public System.Int64 TransactionCodeID { get; set; }

				[JsonPropertyName("EarlyVoluntarilyReport")]
		public System.Boolean EarlyVoluntarilyReport { get; set; }

				[JsonPropertyName("SharesAmount")]
		public System.Int64? SharesAmount { get; set; }

				[JsonPropertyName("DerivativeSecurityPrice")]
		public System.Decimal? DerivativeSecurityPrice { get; set; }

				[JsonPropertyName("TransactionTypeID")]
		public System.Int64? TransactionTypeID { get; set; }

				[JsonPropertyName("DateExercisable")]
		public System.DateTime? DateExercisable { get; set; }

				[JsonPropertyName("ExpirationDate")]
		public System.DateTime? ExpirationDate { get; set; }

				[JsonPropertyName("UnderlyingTitle")]
		public System.String UnderlyingTitle { get; set; }

				[JsonPropertyName("UnderlyingSharesAmount")]
		public System.Int64 UnderlyingSharesAmount { get; set; }

				[JsonPropertyName("AmountFollowingReport")]
		public System.Int64 AmountFollowingReport { get; set; }

				[JsonPropertyName("OwnershipTypeID")]
		public System.Int64 OwnershipTypeID { get; set; }

				[JsonPropertyName("NatureOfIndirectOwnership")]
		public System.String NatureOfIndirectOwnership { get; set; }

				
    }
}
