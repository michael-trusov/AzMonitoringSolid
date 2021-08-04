using AZMA.Core.AzModels;
using System.Collections.Generic;

namespace AZMA.Application.Interfaces
{
    public interface IAlertStandardSchemaAnalyzer
    {
        IEnumerable<IRestCall> Analyze(AlertStandardSchema alertStandardSchema, string originalData);
    }
}
