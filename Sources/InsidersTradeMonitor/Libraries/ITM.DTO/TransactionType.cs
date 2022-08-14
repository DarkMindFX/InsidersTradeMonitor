

using System.Text.Json.Serialization;

namespace ITM.DTO
{
    public class TransactionType : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Code")]
		public System.String Code { get; set; }

				[JsonPropertyName("Description")]
		public System.String Description { get; set; }

				
    }
}
