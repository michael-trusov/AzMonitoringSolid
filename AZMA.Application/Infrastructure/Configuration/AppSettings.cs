using AZMA.Application.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AZMA.Application.Infrastructure.Configuration
{
    public class AppSettings : IAppSettings
    {
        public string TerraformAccount { get; set; }

        public INoiSettings NoiSettings { get; set; } = new NoiSettings();
    }
}
