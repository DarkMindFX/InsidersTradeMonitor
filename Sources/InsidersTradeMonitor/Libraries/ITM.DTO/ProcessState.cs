using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITM.DTO
{
    public class ProcessState
    {
        [JsonPropertyName("ProcessID")]
        public string ProcessID { get; set; }
    }

    public class ProcessStateResponse
    {
        [JsonPropertyName("ProcessID")]
        public string ProcessID { get; set; }

        [JsonPropertyName("StateText")]
        public string StateText { get; set; }
    }
}
