using AZMA.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AZMA.Application.HttpClients
{
    public interface INoiHttpClient
    {
        Task CreateNoiTicketAsync(NoiPayload noiPayload);
    }
}
