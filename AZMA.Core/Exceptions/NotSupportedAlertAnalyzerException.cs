using System;
using System.Collections.Generic;
using System.Text;

namespace AZMA.Core.Exceptions
{
    public class NotSupportedAlertAnalyzerException : Exception
    {
        public NotSupportedAlertAnalyzerException(string message)
            :base(message)
        {
        }
    }
}
