using AZMA.Core.AzModels;
using AZMA.Core.Interfaces;

namespace AZMA.Core.AzModels
{
    /// <summary>
    /// This model declares standard schema of Azure alert. More details you can find here:
    /// https://docs.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-common-schema-definitions
    /// </summary>
    public class AlertStandardSchema
    {
        public AlertStandardSchema()
        {}

        public AlertStandardSchema(string schemaId, AlertStandardSchemaDataEssentials essentials, IAlertStandardSchemaDataContext alertStandardSchemaDataContext)
        {           
            SchemaId = schemaId;

            Data = new AlertStandardSchemaData();
            Data.Essentials = essentials;
            Data.AlertContext = alertStandardSchemaDataContext;
        }

        public string SchemaId { get; set; }

        public AlertStandardSchemaData Data { get; set; }
    }
}
