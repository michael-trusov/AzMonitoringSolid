using System;
using System.Collections.Generic;
using System.Text;

namespace AZMA.Core.Exceptions
{
    public class InvalidNoiPayloadException : Exception
    {
        public InvalidNoiPayloadException(string message)
            : base(message)
        {
        }
    }
}
