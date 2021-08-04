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
using System.Net;
using System.Threading.Tasks;

namespace AZMA.AzFuncActionGroupReceiver
{
    public class FuncNoiNotification
    {
        const string FUNC_VERSION = "1.3";

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
            log.LogInformation($"[apim-noi-notification, v{FUNC_VERSION}] Function was triggered...");

            HttpStatusCode result = HttpStatusCode.OK;
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                log.LogInformation($"[apim-noi-notification, v{FUNC_VERSION}] Request body: {requestBody}");

                var alertStandardSchema = _alertStandardSchemaParser.Parse(requestBody);

                if (alertStandardSchema != null &&
                    alertStandardSchema.Data != null &&
                    alertStandardSchema.Data.Essentials != null)
                {
                    log.LogInformation($"[apim-noi-notification, v{FUNC_VERSION}] Alert schema was parsed successfully.");
                    NoiPayload noiPayload = _noiPayloadService.CreateNoiPayload(alertStandardSchema.Data.Essentials)
                                                              .AppId(_appSettings.NoiSettings.AppId)
                                                              .AlertGroup(_appSettings.NoiSettings.AlertGroup)
                                                              .OriginalAlertPayload(requestBody)
                                                              .CmdbClasses(_appSettings.NoiSettings.CmdbConfig)
                                                              .Validate();

                    var restCallResult = await new CreateSnowTicket(_noiHttpClient, noiPayload).ExecuteAsync();
                    log.LogInformation($"[apim-noi-notification, v{FUNC_VERSION}] REST call was sent to '{restCallResult.RequestUrl}' with request body '{restCallResult.RequestBody}'. Response status code: '{restCallResult.StatusCode}', Response Reason Phrase: '{restCallResult.ReasonPhrase}'");

                    result = restCallResult.StatusCode;
                }

                log.LogInformation($"[apim-noi-notification, v{FUNC_VERSION}] was executed successfully.");

                return new StatusCodeResult((int)result);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);

                return new BadRequestResult();
            }
        }
    }
}
