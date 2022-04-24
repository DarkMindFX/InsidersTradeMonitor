using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITM.DTO
{
    public class RpcStartImport
    {
        [JsonPropertyName("CIK")]
        public string CIK
        {
            get; set;
        }

        [JsonPropertyName("DateFrom")]
        public DateTime DateFrom
        {
            get; set;
        }

        [JsonPropertyName("DateTo")]
        public DateTime DateTo
        {
            get; set;
        }
    }

    public class RpcStartImportResponse
    {
        [JsonPropertyName("ProcessID")]
        public string ProcessID { get; set; }
    }
}
