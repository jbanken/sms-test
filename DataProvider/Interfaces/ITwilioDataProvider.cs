using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces
{
    public interface ITwilioDataProvider
    {
        Task<Entity.TwilioMessage> SaveMessage(Entity.TwilioMessage message);
        Task<Entity.TwilioStatusCallback> SaveStatusCallback(Entity.TwilioStatusCallback record);
        Task<Entity.TwilioIncomingMessageCallback> SaveIncomingMessageCallback(Entity.TwilioIncomingMessageCallback record);
    }
}
