using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DataProvider
{
    public class LogDataProvider : Interfaces.ILogDataProvider
    {
        public async Task<Entity.APILog> LogRequest(Entity.APILog log)
        {
            using (var db = new Data.SMSDataModel())
            {
                log.Id = Guid.NewGuid();
                db.APILogs.Add(log);
                await db.SaveChangesAsync();
                return log;
            }
        }

        public async Task<Entity.APILog> LogResponse(Entity.APILog log)
        {
            using (var db = new Data.SMSDataModel())
            {
                db.APILogs.Attach(log);
                db.Entry(log).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return log;
            }
        }
    }
}
