using System;
using System.Collections.Generic;
using System.Text;

namespace AZMA.Core.Exceptions
{
    public class NoiNotFoundOriginalAlertPayloadException : Exception
    {
        public NoiNotFoundOriginalAlertPayloadException(string message)
            : base(message)
        {
        }
    }
}
