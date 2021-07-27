using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AZMA.Application.Interfaces
{
    public interface IAction
    {
        public Task ExecuteAsync();
    }
}
