

using System.Text.Json.Serialization;

namespace ITM.DTO
{
    public class Entity : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("EntityTypeID")]
		public System.Int64 EntityTypeID { get; set; }

				[JsonPropertyName("CIK")]
		public System.Int32 CIK { get; set; }

				[JsonPropertyName("Name")]
		public System.String Name { get; set; }

				[JsonPropertyName("TradingSymbol")]
		public System.String TradingSymbol { get; set; }

				[JsonPropertyName("IsMonitored")]
		public System.Boolean IsMonitored { get; set; }

				
    }
}
