using AZMA.Application.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AZMA.Application.Infrastructure.Configuration
{
    public interface IAppSettings
    {
        public string TerraformAccount { get; set; }

        public INoiSettings NoiSettings { get; set; }
    }
}
