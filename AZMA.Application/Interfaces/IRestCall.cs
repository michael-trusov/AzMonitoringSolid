using AZMA.Application.Models;
using System.Threading.Tasks;

namespace AZMA.Application.Interfaces
{
    public interface IRestCall
    {
        public Task<RestCallResult> ExecuteAsync();
    }
}
