using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITM.DTO
{
    public class MessageBase
    {
        [JsonPropertyName("Name")]
        public string Name
        {
            get; set;
        }

        [JsonPropertyName("Payload")]
        public string Payload
        {
            get; set;
        }

    }
}
