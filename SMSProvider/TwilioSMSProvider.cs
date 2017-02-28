
using Logger.Interfaces;
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

        public ILogService LogService { get; set; }

        public TwilioSMSProvider(ILogService logService)
        {
            LogService = logService;
        }

        public async Task<Models.SendResponse> Send(Models.SendRequest request)
        {
            var result = new Models.SendResponse();

            var accountSid = System.Configuration.ConfigurationManager.AppSettings["TwilioSID"];
            var authToken = System.Configuration.ConfigurationManager.AppSettings["TwilioAuthToken"];
            var from = System.Configuration.ConfigurationManager.AppSettings["TwilioTestFrom"];

            TwilioClient.Init(accountSid, authToken);
            
            var client = TwilioClient.GetRestClient();
            var log = new Entity.APILog();
            log.RequestContentBody = "To="+request.To+ "&From=" + from + "&Body=" + request.Message;
            log.RequestContentType = "application/json";
            log.RequestHeaders = "accountSid="+accountSid+ "&authToken="+ authToken;
            log.RequestIpAddress = "";
            log.RequestMethod = "POST";
            log.RequestTimeStamp = DateTime.UtcNow;
            log.RequestUri = "https://api.twilio.com/2010-04-01/Accounts/"+ accountSid + "/Messages.json";
            log = await LogService.LogRequest(log);

            MessageResource message;
            try { 
                message = await MessageResource.CreateAsync(
                    to:new PhoneNumber(request.To),
                    from: new PhoneNumber(from),//TODO using test FROM for now
                    body: request.Message
                );
            }catch(Exception ex){
                log.ResponseContentBody = ex.Message;
                log.ResponseContentType = "error";
                log.ResponseHeaders = null;
                log.ResponseStatusCode = 500;
                log.ResponseTimeStamp = DateTime.UtcNow;
                log = await LogService.LogResponse(log);

                return result;
            }

            log.ResponseContentBody = JsonConvert.SerializeObject(message);
            log.ResponseContentType = "application/json";
            log.ResponseHeaders = null;
            log.ResponseStatusCode = 200;
            log.ResponseTimeStamp = DateTime.UtcNow;
            log = await LogService.LogResponse(log);

            return result;
        }
    }
}
