

using System.Text.Json.Serialization;

namespace ITM.DTO
{
    public class User : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("Login")]
        public System.String Login { get; set; }

        [JsonPropertyName("Password")]
        public System.String Password { get; set; }

        [JsonPropertyName("Salt")]
        public System.String Salt { get; set; }

        [JsonPropertyName("FirstName")]
        public System.String FirstName { get; set; }

        [JsonPropertyName("MiddleName")]
        public System.String MiddleName { get; set; }

        [JsonPropertyName("LastName")]
        public System.String LastName { get; set; }

        [JsonPropertyName("FriendlyName")]
        public System.String FriendlyName { get; set; }

        [JsonPropertyName("CreatedDate")]
        public System.DateTime CreatedDate { get; set; }

        [JsonPropertyName("ModifiedDate")]
        public System.DateTime? ModifiedDate { get; set; }

        [JsonPropertyName("ModifiedByID")]
        public System.Int64? ModifiedByID { get; set; }


    }
}
