using AZMA.Core.Interfaces;

namespace AZMA.Core.AzModels
{
    /// <summary>
    /// This model declares standard schema of Azure alert. More details you can find here:
    /// https://docs.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-common-schema-definitions
    /// </summary>
    public class AlertStandardSchemaData
    {
        public AlertStandardSchemaDataEssentials Essentials { get; set; }

        public IAlertStandardSchemaDataContext AlertContext { get; set; }
    }
}
