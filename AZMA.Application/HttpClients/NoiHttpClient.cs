using AZMA.Application.Infrastructure.Configuration;
using AZMA.Application.Models;
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

        public NoiHttpClient(HttpClient httpClient, IAppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;            
        }

        public async Task<RestCallResult> CreateNoiTicketAsync(NoiPayload noiPayload)
        {
            var noiPayloadAsJson = JsonConvert.SerializeObject(noiPayload);

            using (var content = new StringContent(noiPayloadAsJson))
            {
                var httpResponse = await _httpClient.PostAsync(_appSettings.NoiSettings.ServiceEndpoint, content);

                return new RestCallResult
                {
                    Url = _appSettings.NoiSettings.ServiceEndpoint,
                    StatusCode = httpResponse.StatusCode,
                    ReasonPhrase = httpResponse.ReasonPhrase
                };
            }
        }
    }
}
