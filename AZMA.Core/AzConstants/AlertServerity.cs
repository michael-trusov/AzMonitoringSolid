using System;
using System.Collections.Generic;
using System.Text;

namespace AZMA.Core.AzConstants
{
    /// <summary>
    /// More details you can find here:
    /// https://docs.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-overview#overview
    /// </summary>
    public struct AlertServerity
    {
        public const string Sev0 = "Sev0"; // Critical

        public const string Sev1 = "Sev1"; // Error

        public const string Sev2 = "Sev2"; // Warning

        public const string Sev3 = "Sev3"; // Informational

        public const string Sev4 = "Sev4"; // Verbose
    }
}
