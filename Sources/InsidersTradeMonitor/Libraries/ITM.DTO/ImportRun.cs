

using System.Text.Json.Serialization;

namespace ITM.DTO
{
    public class ImportRun : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("TimeStart")]
		public System.DateTime TimeStart { get; set; }

				[JsonPropertyName("TimeEnd")]
		public System.DateTime? TimeEnd { get; set; }

				[JsonPropertyName("RequestJson")]
		public System.String RequestJson { get; set; }

				[JsonPropertyName("StateID")]
		public System.Int64 StateID { get; set; }

				
    }
}
