using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITM.DTO
{
    public class ReportsImported
    {
        [JsonPropertyName("CIK")]
        public string CIK
        {
            get; set;
        }

        [JsonPropertyName("ReportIDs")]
        public IList<long> ReportIDs
        {
            get; set;
        }

        [JsonPropertyName("ImportRunID")]
        public long ImportRunID
        {
            get; set;
        }
    }
}
