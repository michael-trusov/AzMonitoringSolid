using AZMA.Core.AzModels;
using AZMA.Core.Models;

namespace AZMA.Core.Interfaces
{
    public interface INoiPayloadService
    {
        NoiPayloadWrapper CreateNoiPayload(AlertStandardSchemaDataEssentials essentials);
    }
}