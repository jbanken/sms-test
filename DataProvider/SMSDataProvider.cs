
using System.Threading.Tasks;
using System.Data.Entity;
using System;
using Entity;
using System.Collections.Generic;
using System.Linq;
namespace DataProvider
{
    public class SMSDataProvider : Interfaces.ISMSDataProvider
    {
        public async Task<Entity.ThirdPartyService> FindThirdPartyService(string code)
        {
            using (var db = new Data.SMSDataModel())
            {
                return await db.ThirdPartyServices.FirstOrDefaultAsync(tps => tps.Code.Equals(code.ToLower()));
            }
        }

        public async Task<MessageLog> GetById(Guid id)
        {
            using (var db = new Data.SMSDataModel())
            {
                return await db.MessageLogs.FirstOrDefaultAsync(m => m.Id == id);
            }
        }

        public async Task<List<MessageLogStatus>> ListMessageStatuses(Guid id)
        {
            using (var db = new Data.SMSDataModel())
            {
                var results = from s in db.TwilioStatusCallbacks
                              join m in db.MessageLogs on s.MessageSid equals m.ThirdPartyReferenceCode
                              where m.Id == id
                              select new MessageLogStatus
                              {
                                  Name=s.MessageStatus
                              };

                return await results.ToListAsync();
            }
        }

        public async Task<Entity.MessageLog> SaveLog(Entity.MessageLog log)
        {
            using (var db = new Data.SMSDataModel())
            {
                if (log.Id == Guid.Empty)
                {
                    log.Id = Guid.NewGuid();
                    log.CreateDate = DateTime.UtcNow;
                    db.MessageLogs.Add(log);
                }
                else
                {
                    db.MessageLogs.Attach(log);
                    db.Entry(log).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
                return log;
            }
        }
    }
}
