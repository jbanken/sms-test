using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces
{
    public interface ISMSDataProvider
    {
        Task<Entity.MessageLog> SaveLog(Entity.MessageLog log);
        Task<Entity.ThirdPartyService> FindThirdPartyService(string code);
    }
}
