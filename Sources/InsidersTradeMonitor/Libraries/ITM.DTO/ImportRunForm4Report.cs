

using System.Text.Json.Serialization;

namespace ITM.DTO
{
    public class ImportRunForm4Report : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("ImportRunID")]
		public System.Int64 ImportRunID { get; set; }

				[JsonPropertyName("Form4ReportID")]
		public System.Int64 Form4ReportID { get; set; }

				[JsonPropertyName("TimeStarted")]
		public System.DateTime TimeStarted { get; set; }

				[JsonPropertyName("TimeCompleted")]
		public System.DateTime? TimeCompleted { get; set; }

				
    }
}
