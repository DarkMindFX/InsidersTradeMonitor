


using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class OwnershipType : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Code")]
		public System.String Code { get; set; }

				[JsonPropertyName("Description")]
		public System.String Description { get; set; }

				
    }
}
