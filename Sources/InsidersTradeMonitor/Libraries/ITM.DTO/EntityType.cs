


using System.Text.Json.Serialization;

namespace PPT.DTO
{
    public class EntityType : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("TypeName")]
		public System.String TypeName { get; set; }

				
    }
}
