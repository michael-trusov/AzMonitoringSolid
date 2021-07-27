using AZMA.Application.Infrastructure.Configuration;
using AZMA.Application.Infrastructure.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: FunctionsStartup(typeof(AZMA.AzFuncActionGroupReceiver.Startup))]

namespace AZMA.AzFuncActionGroupReceiver
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var appSettings = new AppSettings();
            InitializeAppSettings(appSettings, builder.GetContext().Configuration);

            builder.Services.AddSingleton<IAppSettings>(appSettings);
           
            builder.Services.AddApplicationServices();
        }

        private void InitializeAppSettings(IAppSettings appSettings, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("TERRAFORM_ACCOUNT");
            if (configSection != null)
                appSettings.TerraformAccount = configSection.Value;

            InitializeNoiSettings(appSettings.NoiSettings, configuration);
        }

        private void InitializeNoiSettings(INoiSettings noiSettings, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("NOI_ENDPOINT");
            if (configSection != null)
                noiSettings.ServiceEndpoint = configSection.Value;

            configSection = configuration.GetSection("NOI_APP_ID");
            if (configSection != null)
                noiSettings.AppId = configSection.Value;

            configSection = configuration.GetSection("NOI_ALERT_GROUP");
            if (configSection != null)
                noiSettings.AlertGroup = configSection.Value;

            configSection = configuration.GetSection("CMDB_CONFIG");
            if (configSection != null)
                noiSettings.CmdbConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(configSection.Value.ToLower());
        }
    }
}
