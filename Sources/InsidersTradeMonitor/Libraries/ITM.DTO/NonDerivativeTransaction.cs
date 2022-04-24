


using System.Text.Json.Serialization;

namespace ITM.DTO
{
    public class NonDerivativeTransaction : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("Form4ReportID")]
        public System.Int64 Form4ReportID { get; set; }

        [JsonPropertyName("TitleOfSecurity")]
        public System.String TitleOfSecurity { get; set; }

        [JsonPropertyName("TransactionDate")]
        public System.DateTime TransactionDate { get; set; }

        [JsonPropertyName("DeemedExecDate")]
        public System.DateTime? DeemedExecDate { get; set; }

        [JsonPropertyName("TransactionCodeID")]
        public System.Int64 TransactionCodeID { get; set; }

        [JsonPropertyName("EarlyVoluntarilyReport")]
        public System.Boolean EarlyVoluntarilyReport { get; set; }

        [JsonPropertyName("SharesAmount")]
        public System.Int64? SharesAmount { get; set; }

        [JsonPropertyName("TransactionTypeID")]
        public System.Int64 TransactionTypeID { get; set; }

        [JsonPropertyName("Price")]
        public System.Decimal Price { get; set; }

        [JsonPropertyName("AmountFollowingReport")]
        public System.Int64 AmountFollowingReport { get; set; }

        [JsonPropertyName("OwnershipTypeID")]
        public System.Int64 OwnershipTypeID { get; set; }

        [JsonPropertyName("NatureOfIndirectOwnership")]
        public System.String NatureOfIndirectOwnership { get; set; }


    }
}
