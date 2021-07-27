using System;
using System.Collections.Generic;
using System.Text;

namespace AZMA.Core.Exceptions
{
    public class NotFoundCmdbClassException : Exception
    {
        public NotFoundCmdbClassException(string message)
            : base(message)
        {
        }

    }
}
