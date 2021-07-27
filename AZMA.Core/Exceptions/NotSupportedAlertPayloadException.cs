using System;

namespace AZMA.Core.Exceptions
{
    public class NotSupportedAlertPayloadException : Exception
    {
        public NotSupportedAlertPayloadException(string requestContent, Exception innerException)
            :base(requestContent)
        {
        }
    }
}
