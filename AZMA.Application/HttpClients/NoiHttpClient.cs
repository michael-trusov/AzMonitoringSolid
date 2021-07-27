using AZMA.Application.Infrastructure.Configuration;
using AZMA.Core.Interfaces;
using AZMA.Core.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace AZMA.Application.HttpClients
{
    class NoiHttpClient : INoiHttpClient
    {
        private HttpClient _httpClient;
        private IAppSettings _appSettings;
        private ILogger<NoiHttpClient> _logger;

        public NoiHttpClient(HttpClient httpClient, IAppSettings appSettings, ILogger<NoiHttpClient> logger)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
            _logger = logger;
        }

        public async Task CreateNoiTicketAsync(NoiPayload noiPayload)
        {
            var noiPayloadAsJson = JsonConvert.SerializeObject(noiPayload);

            using (var content = new StringContent(noiPayloadAsJson))
            {
                var httpResponse = await _httpClient.PostAsync(_appSettings.NoiSettings.ServiceEndpoint, content);

                _logger.LogInformation($"Status Code: {httpResponse.StatusCode}, Reason Phrase: {httpResponse.ReasonPhrase}");
            }
        }
    }
}
