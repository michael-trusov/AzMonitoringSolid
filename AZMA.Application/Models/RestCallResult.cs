using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AZMA.Application.Models
{
    public class RestCallResult
    {
        public string RequestUrl { get; set; }

        public string RequestBody { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string ReasonPhrase { get; set; }

        
    }
}
