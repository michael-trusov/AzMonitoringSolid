using AZMA.Application.HttpClients;
using AZMA.Application.Interfaces;
using AZMA.Application.Services;
using AZMA.Core.Interfaces;
using AZMA.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AZMA.Application.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAlertAnalyzersFactory, AlertAnalyzersFactory>();

            serviceCollection.AddTransient<INoiPayloadService, NoiPayloadService>();

            serviceCollection.AddTransient<IAlertStandardSchemaParser, AlertStandardSchemaParser>();
            serviceCollection.AddTransient<ActivityLogAdministrativeAlertAnalyzer>();
            serviceCollection.AddHttpClient<INoiHttpClient, NoiHttpClient>();
           
            return serviceCollection;
        }
    }
}
