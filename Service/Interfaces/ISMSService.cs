
using Entity;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISMSService
    {
        Task<MessageLog> SaveLog(MessageLog log);
        Task<Entity.MessageLog> Send(Models.SendRequest request);
        Task<Entity.ThirdPartyService> FindThirdPartyService(string code);
    }
}
