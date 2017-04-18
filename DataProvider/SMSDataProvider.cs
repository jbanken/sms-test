﻿
using System.Threading.Tasks;
using System.Data.Entity;
using System;
using Entity;

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
