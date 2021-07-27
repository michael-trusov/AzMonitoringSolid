using AZMA.Core.AzModels;

namespace AZMA.Core.Interfaces
{
    public interface IAlertStandardSchemaParser
    {
        public AlertStandardSchema Parse(string content);
    }
}
