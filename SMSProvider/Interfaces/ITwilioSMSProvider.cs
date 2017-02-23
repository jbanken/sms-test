using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSProvider.Interfaces
{
    public interface ITwilioSMSProvider : ISMSProvider
    {
        Task<Models.SendResponse> Send(Models.SendRequest request);
    }
}
