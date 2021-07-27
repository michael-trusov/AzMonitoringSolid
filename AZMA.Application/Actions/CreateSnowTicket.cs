using AZMA.Application.HttpClients;
using AZMA.Application.Interfaces;
using AZMA.Core.Models;
using System.Threading.Tasks;

namespace AZMA.Application.Commands
{
    public class CreateSnowTicket : IAction
    {
        INoiHttpClient _noiHttpClient;
        NoiPayload _noiPayload;

        public CreateSnowTicket(INoiHttpClient noiHttpClient, NoiPayload noiPayload)
        {
            _noiHttpClient = noiHttpClient;
            _noiPayload = noiPayload;
        }

        public async Task ExecuteAsync()
        {           
            await _noiHttpClient.CreateNoiTicketAsync(_noiPayload);            
        }
    }
}
