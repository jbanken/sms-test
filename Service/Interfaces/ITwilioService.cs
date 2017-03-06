using System.Threading.Tasks;
using SMSProvider.Models;
namespace Service.Interfaces
{
    public interface ITwilioService
    {
        Task<Entity.TwilioMessage> Send(Models.SendRequest request);
        Task<Entity.TwilioMessage> SaveMessage(Entity.TwilioMessage message);
        Task<Entity.TwilioStatusCallback> SaveStatusCallback(Entity.TwilioStatusCallback record);
        Task<Entity.TwilioIncomingMessageCallback> SaveIncomingMessageCallback(Entity.TwilioIncomingMessageCallback record);
    }
}
