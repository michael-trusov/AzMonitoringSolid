using AZMA.Core.Interfaces;

namespace AZMA.Core.AzModels.AlertContexts
{
    public class ActivityLogAdministrativeAlertContext : IAlertStandardSchemaDataContext
    {
        public struct ActivityLogAdministrativeAuthorization
        {
            public string Action { get; set; }

            public string Scope { get; set; }
        }

        public ActivityLogAdministrativeAuthorization Authorization { get; set; }

        public string Channels { get; set; }

        public string Caller { get; set; }

        public string EventSource { get; set; }

        public string OperationName { get; set; }
    }
}
