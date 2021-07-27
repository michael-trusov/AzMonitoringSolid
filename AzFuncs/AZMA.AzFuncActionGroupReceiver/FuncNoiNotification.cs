using AZMA.Application.Commands;
using AZMA.Application.HttpClients;
using AZMA.Application.Infrastructure.Configuration;
using AZMA.Core.Interfaces;
using AZMA.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AZMA.AzFuncActionGroupReceiver
{
    public class FuncNoiNotification
    {
        private IAppSettings _appSettings;

        private IAlertStandardSchemaParser _alertStandardSchemaParser;

        private INoiHttpClient _noiHttpClient;
        private INoiPayloadService _noiPayloadService;

        public FuncNoiNotification(IAppSettings appSettings,
                                             IAlertStandardSchemaParser alertStandardSchemaParser,                                             
                                             INoiHttpClient noiHttpClient,
                                             INoiPayloadService noiPayloadService)
        {
            _appSettings = appSettings;

            _alertStandardSchemaParser = alertStandardSchemaParser;

            _noiHttpClient = noiHttpClient;
            _noiPayloadService = noiPayloadService;
        }

        [FunctionName("apim-noi-notification")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("'apim-noi-notification' was triggered...");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                log.LogInformation($"Request body: {requestBody}");

                var alertStandardSchema = _alertStandardSchemaParser.Parse(requestBody);

                if (alertStandardSchema != null &&
                    alertStandardSchema.Data != null &&
                    alertStandardSchema.Data.Essentials != null)
                {
                    NoiPayload noiPayload = _noiPayloadService.CreateNoiPayload(alertStandardSchema.Data.Essentials)
                                                              .AppId(_appSettings.NoiSettings.AppId)
                                                              .AlertGroup(_appSettings.NoiSettings.AlertGroup)
                                                              .OriginalAlertPayload(requestBody)
                                                              .CmdbClasses(_appSettings.NoiSettings.CmdbConfig)
                                                              .Validate();

                    await new CreateSnowTicket(_noiHttpClient, noiPayload).ExecuteAsync();
                }

                log.LogInformation("'apim-noi-notification' was executed successfully.");

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);

                return new BadRequestResult();
            }
        }
    }
}
