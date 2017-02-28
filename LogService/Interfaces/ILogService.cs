using System.Threading.Tasks;

namespace Logger.Interfaces
{
    public interface ILogService
    {
        Task<Entity.APILog> LogRequest(Entity.APILog log);
        Task<Entity.APILog> LogResponse(Entity.APILog log);
    }
}
