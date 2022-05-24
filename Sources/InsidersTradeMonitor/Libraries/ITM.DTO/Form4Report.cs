


using System.Text.Json.Serialization;

namespace ITM.DTO
{
    public class Form4Report : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("IssuerID")]
        public System.Int64 IssuerID { get; set; }

        [JsonPropertyName("ReporterID")]
        public System.Int64 ReporterID { get; set; }

        [JsonPropertyName("IsOfficer")]
        public System.Boolean IsOfficer { get; set; }

        [JsonPropertyName("IsDirector")]
        public System.Boolean IsDirector { get; set; }

        [JsonPropertyName("Is10PctHolder")]
        public System.Boolean Is10PctHolder { get; set; }

        [JsonPropertyName("IsOther")]
        public System.Boolean IsOther { get; set; }

        [JsonPropertyName("OtherText")]
        public System.String OtherText { get; set; }

        [JsonPropertyName("OfficerTitle")]
        public System.String OfficerTitle { get; set; }

        [JsonPropertyName("Date")]
        public System.DateTime Date { get; set; }


    }
}
