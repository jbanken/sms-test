
using Newtonsoft.Json;
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
            TwilioClient.Init(request.AccountSid, request.AuthToken);
            var client = TwilioClient.GetRestClient();

            MessageResource message;

            message = await MessageResource.CreateAsync(
                to: new PhoneNumber(request.To),
                from: new PhoneNumber(request.From),
                body: request.Message,
                statusCallback: new Uri(request.StatusCallbackURL));

            result.AccountSid = message.AccountSid;
            result.APIVersion = message.ApiVersion;
            result.Body = message.Body;
            result.DateCreated = message.DateCreated.ToString();
            result.DateSent = message.DateSent.ToString();
            result.DateUpdated = message.DateUpdated.ToString();
            result.Direction = message.Direction.ToString();
            result.ErrorCode = message.ErrorCode.ToString();
            result.ErrorMessage = message.ErrorMessage;
            result.From = message.From.ToString();
            result.MessageServiceSid = message.MessagingServiceSid;
            result.NumMedia = message.NumMedia;
            result.NumSegments = message.NumSegments;
            result.Price = message.Price.ToString();
            result.PriceUnit = message.PriceUnit;
            result.Sid = message.Sid;
            result.Status = message.Status.ToString();
            result.To = message.To;
            result.Uri = message.Uri;

            return result;
        }
    }
}
