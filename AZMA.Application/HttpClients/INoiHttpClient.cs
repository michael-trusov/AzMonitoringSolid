using AZMA.Application.Models;
using AZMA.Core.Models;
using System.Threading.Tasks;

namespace AZMA.Application.HttpClients
{
    public interface INoiHttpClient
    {
        Task<RestCallResult> CreateNoiTicketAsync(NoiPayload noiPayload);
    }
}
