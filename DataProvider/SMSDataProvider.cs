
using System.Threading.Tasks;
using Entity;
using System.Data.Entity;
namespace DataProvider
{
    public class SMSDataProvider : Interfaces.ISMSDataProvider
    {
        public async Task<ThirdPartyService> FindThirdPartyService(string code)
        {
            using (var db = new Data.SMSDataModel())
            {
                return await db.ThirdPartyServices.FirstOrDefaultAsync(tps => tps.Code.Equals(code.ToLower()));
            }
        }
        public async Task<MessageLog> Log(MessageLog log)
        {
            using(var db = new Data.SMSDataModel())
            {
                db.MessageLogs.Add(log);
                await db.SaveChangesAsync();
                return log;
            }
        }
    }
}
