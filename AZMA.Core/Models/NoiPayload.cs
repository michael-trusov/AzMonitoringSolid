using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AZMA.Core.Models
{
    public class NoiPayload
    {
        [JsonPropertyName("app_id")]
        public string AppId { get; set; }

        public string Node { get; set; }

        public string AlertGroup { get; set; }

        public string Severity { get; set; }

        public string Summary { get; set; }

        [JsonPropertyName("Work_notes")]
        public string OriginalAlertPayload { get; set; }

        [JsonPropertyName("Current_status")]
        public string Status { get; set; }

        [JsonPropertyName("Alert_Identifier")]
        public string AlertId {get;set;}

        [JsonPropertyName("Alert_Name")]
        public string AlertName { get; set; }

        [JsonPropertyName("Servicenow_cmdb_class")]
        public string ServiceNowCmdbClass { get; set; }

        [JsonProperty("Servicenow_ci")]
        public string ServiceNowCi { get; set; }

    }
}
