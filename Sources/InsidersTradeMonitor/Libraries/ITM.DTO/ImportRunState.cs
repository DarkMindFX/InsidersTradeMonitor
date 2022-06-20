

using System.Text.Json.Serialization;

namespace ITM.DTO
{
    public class ImportRunState : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Name")]
		public System.String Name { get; set; }

				
    }
}
