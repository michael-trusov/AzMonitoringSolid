using AZMA.Application.Infrastructure.Configuration;
using AZMA.Application.Interfaces;
using AZMA.Core.Interfaces;
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
    public class FuncLogAlertAnalyzer
    {
        private IAppSettings _appSettings;

        private IAlertStandardSchemaParser _alertStandardSchemaParser;
        private IAlertAnalyzersFactory _alertAnalyzersFactory;

        public FuncLogAlertAnalyzer(IAppSettings appSettings, 
                                             IAlertStandardSchemaParser alertStandardSchemaParser,                                              
                                             IAlertAnalyzersFactory alertAnalyzersFactory)
        {
            _appSettings = appSettings;

            _alertStandardSchemaParser = alertStandardSchemaParser;
            
            _alertAnalyzersFactory = alertAnalyzersFactory;
        }

        [FunctionName("apim-log-alert-analyzer")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("'apim-log-alert-analyzer' was triggered...");

            try
            {

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                log.LogInformation($"Request body: {requestBody}");

                var alertStandardSchema = _alertStandardSchemaParser.Parse(requestBody);

                if (alertStandardSchema != null &&
                    alertStandardSchema.Data != null &&
                    alertStandardSchema.Data.AlertContext != null)
                {

                    var actions = _alertAnalyzersFactory.CreateFor(alertStandardSchema.Data.AlertContext)
                                                        .Analyze(alertStandardSchema, requestBody);
                    foreach (var action in actions)
                    {
                        await action.ExecuteAsync();
                    }
                }

                log.LogInformation("'apim-log-alert-analyzer' was executed successfully.");

                return new OkResult();
            }
            catch(Exception ex)
            {
                log.LogError(ex, ex.Message);

                return new BadRequestResult();
            }
        }
    }
}

