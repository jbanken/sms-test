using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DataProvider
{
    public class TwilioDataProvider : Interfaces.ITwilioDataProvider
    {
        public async Task<TwilioIncomingMessageCallback> SaveIncomingMessageCallback(TwilioIncomingMessageCallback record)
        {
            using (var db = new Data.SMSDataModel())
            {
                if (record.Id == Guid.Empty)
                {
                    record.Id = Guid.NewGuid();
                    db.TwilioIncomingMessageCallbacks.Add(record);
                }
                else
                {
                    db.TwilioIncomingMessageCallbacks.Attach(record);
                }

                await db.SaveChangesAsync();
                return record;
            }
        }

        public async Task<TwilioMessage> SaveMessage(TwilioMessage message)
        {
            using (var db = new Data.SMSDataModel())
            {
                if (message.Id == Guid.Empty)
                {
                    message.Id = Guid.NewGuid();
                    db.TwilioMessages.Add(message);
                }
                else
                {
                    db.TwilioMessages.Attach(message);
                }

                await db.SaveChangesAsync();
                return message;
            }
        }

        public async Task<TwilioStatusCallback> SaveStatusCallback(TwilioStatusCallback record)
        {
            using (var db = new Data.SMSDataModel())
            {
                if (record.Id == Guid.Empty)
                {
                    record.Id = Guid.NewGuid();
                    db.TwilioStatusCallbacks.Add(record);
                }
                else
                {
                    db.TwilioStatusCallbacks.Attach(record);
                }

                await db.SaveChangesAsync();
                return record;
            }
        }
    }
}
