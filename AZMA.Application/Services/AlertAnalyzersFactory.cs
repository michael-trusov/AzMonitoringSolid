using AZMA.Core.AzModels.AlertContexts;
using AZMA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using AZMA.Core.Exceptions;
using AZMA.Application.Interfaces;

namespace AZMA.Application.Services
{
    public class AlertAnalyzersFactory : IAlertAnalyzersFactory
    {
        private IServiceProvider _serviceProvider;

        public AlertAnalyzersFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IAlertStandardSchemaAnalyzer CreateFor(IAlertStandardSchemaDataContext alertContext)
        {
            if (alertContext is ActivityLogAdministrativeAlertContext)
            {
                return _serviceProvider.GetService<ActivityLogAdministrativeAlertAnalyzer>();
            }

            throw new NotSupportedAlertAnalyzerException($"Analyzer can't be created for 'AlertContext' of type '{alertContext.GetType()}'");
        }
    }
}
