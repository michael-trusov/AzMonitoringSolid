using AZMA.Application.Commands;
using AZMA.Application.HttpClients;
using AZMA.Application.Infrastructure.Configuration;
using AZMA.Application.Interfaces;
using AZMA.Core.AzConstants;
using AZMA.Core.AzModels;
using AZMA.Core.AzModels.AlertContexts;
using AZMA.Core.Interfaces;
using AZMA.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace AZMA.Application.Services
{
    class ActivityLogAdministrativeAlertAnalyzer : IAlertStandardSchemaAnalyzer
    {       
        private const string MsApiManagementWriteOperation = "microsoft.apimanagement/service/write";

        private INoiPayloadService _noiPayloadService;

        private INoiHttpClient _noiHttpClient;
        private IAppSettings _appSettings;

        public ActivityLogAdministrativeAlertAnalyzer(INoiPayloadService noiPayloadService, INoiHttpClient noiHttpClient, IAppSettings appSettings)
        {
            _noiPayloadService = noiPayloadService;
            _appSettings = appSettings;
            _noiHttpClient = noiHttpClient;
        }

        public IEnumerable<IAction> Analyze(AlertStandardSchema alertStandardSchema, string originalData)
        {
            List<IAction> commands = new List<IAction>();

            var isApiManagementTargetResource = alertStandardSchema.Data.Essentials.AlertTargetIDs
                                                                                   .Any(e => e.ToLower().Contains(AlertTargetIDs.MsApiManagement_ResourceId));
            if (isApiManagementTargetResource)
            {
                var alertContext = alertStandardSchema.Data.AlertContext as ActivityLogAdministrativeAlertContext;

                var isItWriteOperation = alertContext.OperationName.ToLower().Contains(MsApiManagementWriteOperation);
                if (isItWriteOperation)
                {
                    var IsOperationInitiatedByTerraform = alertContext.Caller.Equals("Azure Platform", System.StringComparison.OrdinalIgnoreCase);                    
                    if (!IsOperationInitiatedByTerraform)
                    {
                        NoiPayload noiPayload = _noiPayloadService.CreateNoiPayload(alertStandardSchema.Data.Essentials)
                                                                  .AppId(_appSettings.NoiSettings.AppId)
                                                                  .AlertGroup(_appSettings.NoiSettings.AlertGroup)
                                                                  .OriginalAlertPayload(originalData)
                                                                  .CmdbClasses(_appSettings.NoiSettings.CmdbConfig)
                                                                  .Validate();

                        var noiNotificationCommand = new CreateSnowTicket(_noiHttpClient, noiPayload);
                                                
                        commands.Add(noiNotificationCommand);
                    }
                }
            }
           
            return commands;
        }
    }
}
