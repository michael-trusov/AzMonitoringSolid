using System;
using System.Collections.Generic;
using System.Text;

namespace AZMA.Core.AzConstants
{
    public struct AlertContext
    {
        public const string Metric = "Platform";

        public const string LogAlert_LogAnalytics = "Log Analytics";

        public const string LogAlert_ApplicationInsights = "Application Insights";

        public const string LogAlert_LogAlertsV2 = "Log Alerts V2";

        public const string ActivityLog_Administrative = "Activity Log - Administrative";

        public const string ActivityLog_Policy = "Activity Log - Policy";

        public const string ActivityLog_Autoscale = "Activity Log - Autoscale";

        public const string ActivityLog_Security = "Activity Log - Security";

        public const string ActivityLog_ServiceHealth = "ServiceHealth";

        public const string ActivityLog_ResourceHealth = "Resource Health";
    }
}
