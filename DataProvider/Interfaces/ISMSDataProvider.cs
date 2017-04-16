using System;
using System.Threading.Tasks;

namespace DataProvider.Interfaces
{
    public interface ISMSDataProvider
    {
        Task<Entity.MessageLog> SaveLog(Entity.MessageLog log);
        Task<Entity.ThirdPartyService> FindThirdPartyService(string code);
        Task<Entity.MessageLog> GetById(Guid id);
    }
}
