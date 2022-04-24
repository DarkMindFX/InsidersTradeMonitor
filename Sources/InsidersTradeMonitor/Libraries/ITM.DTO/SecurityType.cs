


using System.Text.Json.Serialization;

namespace ITM.DTO
{
    public class SecurityType : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("SecurityTypeName")]
		public System.String SecurityTypeName { get; set; }

				
    }
}
