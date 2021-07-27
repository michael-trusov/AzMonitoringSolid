using System;
using System.Collections.Generic;
using System.Text;

namespace AZMA.Application.Infrastructure.Configuration
{
    public interface INoiSettings
    {
        public string ServiceEndpoint { get; set; }

        public string AppId { get; set; }

        public string AlertGroup { get; set; }
       
        public Dictionary<string, string> CmdbConfig { get; set; }
    }
}
