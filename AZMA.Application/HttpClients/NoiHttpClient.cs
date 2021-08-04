using AZMA.Application.Infrastructure.Configuration;
using AZMA.Application.Models;
using AZMA.Core.Interfaces;
using AZMA.Core.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace AZMA.Application.HttpClients
{
    class NoiHttpClient : INoiHttpClient
    {
        private HttpClient _httpClient;
        private IAppSettings _appSettings;        

        public NoiHttpClient(HttpClient httpClient, IAppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;            
        }

        public async Task<RestCallResult> CreateNoiTicketAsync(NoiPayload noiPayload)
        {
            var noiPayloadAsJson = JsonSerializer.Serialize(noiPayload);

            using (var content = new StringContent(noiPayloadAsJson, System.Text.Encoding.UTF8, "application/json"))
            {
                var httpResponse = await _httpClient.PostAsync(_appSettings.NoiSettings.ServiceEndpoint, content);

                return new RestCallResult
                {
                    RequestUrl = _appSettings.NoiSettings.ServiceEndpoint,
                    RequestBody = noiPayloadAsJson,
                    StatusCode = httpResponse.StatusCode,
                    ReasonPhrase = httpResponse.ReasonPhrase
                };
            }
        }
    }
}
