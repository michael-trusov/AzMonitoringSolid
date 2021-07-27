using System;
using System.Collections.Generic;

namespace AZMA.Core.AzModels
{
    public class AlertStandardSchemaDataEssentials
    {
        /// <summary>
        /// The unique resource ID identifying the alert instance.
        /// </summary>
        public string AlertId { get; set; }

        /// <summary>
        /// The name of the alert rule that generated the alert instance.
        /// </summary>
        public string AlertRule { get; set; }

        public string Severity { get; set; }

        /// <summary>
        /// The monitoring service or solution that generated the alert. The fields for the alert context are dictated by the monitoring service.
        /// </summary>
        public string MonitoringService { get; set; }

        public string SignalType { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> AlertTargetIDs { get; set; }

        /// <summary>
        /// The date and time when the alert instance was fired in Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime FiredDateTime { get; set; }
    }
}
