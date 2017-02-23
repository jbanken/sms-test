
using System.Threading.Tasks;

namespace SMSProvider.Interfaces
{
    public interface ISMSProvider
    {
        Task<Models.SendResponse> Send(Models.SendRequest request);
    }
}
