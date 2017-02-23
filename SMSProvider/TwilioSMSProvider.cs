
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SMSProvider
{
    public class TwilioSMSProvider : Interfaces.ITwilioSMSProvider
    {
        public async Task<Models.SendResponse> Send(Models.SendRequest request)
        {
            var result = new Models.SendResponse();

            var accountSid = System.Configuration.ConfigurationManager.AppSettings["TwilioSID"];
            var authToken = System.Configuration.ConfigurationManager.AppSettings["TwilioAuthToken"];
            var from = System.Configuration.ConfigurationManager.AppSettings["TwilioTestFrom"];

            TwilioClient.Init(accountSid, authToken);

            //TODO log pre twilio call
            var message = await MessageResource.CreateAsync(
                to:new PhoneNumber(request.To),
                from: new PhoneNumber(from),//TODO using test FROM for now
                body: request.Message
            );
            //TODO log post twilio call
            return result;
        }
    }
}
