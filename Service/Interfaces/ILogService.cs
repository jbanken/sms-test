using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ILogService
    {
        Task<Entity.APILog> LogRequest(Entity.APILog log);
        Task<Entity.APILog> LogResponse(Entity.APILog log);
    }
}
