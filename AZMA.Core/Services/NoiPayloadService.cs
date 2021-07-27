using AZMA.Core.AzModels;
using AZMA.Core.Interfaces;
using AZMA.Core.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System.Linq;

namespace AZMA.Core.Services
{
    public class NoiPayloadService : INoiPayloadService
    {
        public NoiPayloadWrapper CreateNoiPayload(AlertStandardSchemaDataEssentials essentials)
        {
            ResourceId targetResourceId = ResourceId.FromString(essentials.AlertTargetIDs.First());

            return new NoiPayloadWrapper(new NoiPayload
            {
                AlertId = essentials.AlertId,
                AlertName = essentials.AlertRule,
                Severity = ToNoiSeverity(essentials.Severity),
                Summary = $"({essentials.AlertId}){(essentials.Description ?? "<No description>")}",
                Status = "open",
                Node = targetResourceId.FullResourceType.ToLower(),
                ServiceNowCi = targetResourceId.Name.ToLower()
            });
        }

        private string ToNoiSeverity(string azureSeverityLevel)
        {
            var azureSeverity = azureSeverityLevel.ToLower();

            if (azureSeverity == "sev0" || azureSeverity == "sev1")
            {
                return "CRITICAL";
            }
            else if (azureSeverity == "sev2")
            {
                return "MAJOR";
            }
            else if (azureSeverity == "sev3")
            {
                return "MINOR";
            }
            else
            {
                return "CLEAR";
            }
        }
    }
}
