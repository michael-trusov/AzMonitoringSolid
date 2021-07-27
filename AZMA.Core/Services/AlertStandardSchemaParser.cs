using AZMA.Core.AzConstants;
using AZMA.Core.AzModels;
using AZMA.Core.AzModels.AlertContexts;
using AZMA.Core.Exceptions;
using AZMA.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;

namespace AZMA.Core.Services
{
    public class AlertStandardSchemaParser : IAlertStandardSchemaParser
    {
        /// <summary>
        /// Parse 'Essentials' section (common for any type of alert), define type of alert and depends on alert type 
        /// parse 'AlertContext' section
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public AlertStandardSchema Parse(string content)
        {
            try
            {
                JObject jsonAlertStandardSchema = JObject.Parse(content);                

                JToken jsonEssentialsSection = jsonAlertStandardSchema["data"]["essentials"];
                var alertStandardSchemaDataEssentials = jsonEssentialsSection.ToObject<AlertStandardSchemaDataEssentials>();

                IAlertStandardSchemaDataContext alertContext = null;
                switch(alertStandardSchemaDataEssentials.MonitoringService)
                {
                    case AlertContext.Metric:
                        alertContext = jsonAlertStandardSchema["data"]["alertContext"].ToObject<MetricAlertContext>();
                        break;
                    case AlertContext.ActivityLog_Administrative:
                        alertContext = jsonAlertStandardSchema["data"]["alertContext"].ToObject<ActivityLogAdministrativeAlertContext>();
                        break;
                }
                
                return new AlertStandardSchema(jsonAlertStandardSchema["schemaId"].Value<string>(), alertStandardSchemaDataEssentials, alertContext);
            }
            catch(NullReferenceException ex)
            {
                throw new NotSupportedAlertPayloadException("Input payload of the alert is not matched with Azure alert standard schema.", ex);
            }
        }
    }
}
