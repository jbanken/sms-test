using System;
using System.Threading.Tasks;
using Entity;
using DataProvider.Interfaces;
using SMSProvider.Interfaces;
using Newtonsoft.Json;
using Logger.Interfaces;

namespace Service
{
    public class TwilioService : Interfaces.ITwilioService
    {
        public ITwilioDataProvider TwilioDataProvider { get; set; }
        public ITwilioSMSProvider TwilioSMSProvider { get; set; }
        public ILogService LogService { get; set; }

        public TwilioService(ITwilioDataProvider twilioDataProvider, ITwilioSMSProvider twilioSMSProvider, ILogService logService)
        {
            TwilioDataProvider = twilioDataProvider;
            TwilioSMSProvider = twilioSMSProvider;
            LogService = logService;
        }

        public async Task<Entity.TwilioMessage> Send(Models.SendRequest request)
        {
            var accountSid = System.Configuration.ConfigurationManager.AppSettings["TwilioSID"];
            var authToken = System.Configuration.ConfigurationManager.AppSettings["TwilioAuthToken"];
            var from = System.Configuration.ConfigurationManager.AppSettings["TwilioTestFrom"];
            var twilioStatusCallBackURL = System.Configuration.ConfigurationManager.AppSettings["TwilioStatusCallback"];
            var twilioBaseURL = System.Configuration.ConfigurationManager.AppSettings["TwilioBaseURL"];

            var twilioRequest = new SMSProvider.Models.SendRequest();
            twilioRequest.To = request.To;
            twilioRequest.From = request.From;
            twilioRequest.ReferenceCode = request.ReferenceCode;
            twilioRequest.Message = request.Message;
            twilioRequest.AccountSid = accountSid;
            twilioRequest.AuthToken = authToken;
            twilioRequest.StatusCallbackURL = twilioStatusCallBackURL;

            var log = new Entity.APILog();
            log.RequestContentBody = "To=" + twilioRequest.To + "&From=" + from + "&Body=" + twilioRequest.Message;
            log.RequestContentType = "application/json";
            log.RequestHeaders = "accountSid=" + accountSid + "&authToken=" + authToken;
            log.RequestIpAddress = "";
            log.RequestMethod = "POST";
            log.RequestTimeStamp = DateTime.UtcNow;
            log.RequestUri = twilioBaseURL + accountSid + "/Messages.json";
            log = await LogService.LogRequest(log);

            var twilioMessage = new Entity.TwilioMessage();
            var sendResponse = new SMSProvider.Models.SendResponse();
            try {
                //call twilio
                sendResponse = await TwilioSMSProvider.Send(twilioRequest);
            }
            catch(Exception ex){
                log.ResponseContentBody = ex.Message;
                log.ResponseContentType = "error";
                log.ResponseHeaders = null;
                log.ResponseStatusCode = 500;
                log.ResponseTimeStamp = DateTime.UtcNow;
                log = await LogService.LogResponse(log);
                return twilioMessage;
            }

            twilioMessage.AccountSid = sendResponse.AccountSid;
            twilioMessage.APIVersion = sendResponse.APIVersion;
            twilioMessage.Body = sendResponse.Body;
            twilioMessage.DateCreated = sendResponse.DateCreated;
            twilioMessage.DateSent = sendResponse.DateSent;
            twilioMessage.Direction = sendResponse.Direction;
            twilioMessage.ErrorCode = sendResponse.ErrorCode;
            twilioMessage.From = sendResponse.From;
            twilioMessage.MessageServiceSid = sendResponse.MessageServiceSid;
            twilioMessage.NumMedia = sendResponse.NumMedia;
            twilioMessage.NumSegments = sendResponse.NumSegments;
            twilioMessage.Price = sendResponse.Price;
            twilioMessage.PriceUnit = sendResponse.PriceUnit;
            twilioMessage.Sid = sendResponse.Sid;
            twilioMessage.Status = sendResponse.Status;
            twilioMessage.Uri = sendResponse.Uri;
            twilioMessage.To = sendResponse.To;
            twilioMessage = await SaveMessage(twilioMessage);

            log.ResponseContentBody = JsonConvert.SerializeObject(twilioMessage);
            log.ResponseContentType = "application/json";
            log.ResponseHeaders = null;
            log.ResponseStatusCode = 200;
            log.ResponseTimeStamp = DateTime.UtcNow;
            log = await LogService.LogResponse(log);

            return twilioMessage;
        }

        public async Task<TwilioIncomingMessageCallback> SaveIncomingMessageCallback(TwilioIncomingMessageCallback record)
        {
            record.CreateDate = DateTime.UtcNow;
            return await TwilioDataProvider.SaveIncomingMessageCallback(record);
        }

        public async Task<TwilioMessage> SaveMessage(TwilioMessage message)
        {
            message.CreateDate = DateTime.UtcNow;
            return await TwilioDataProvider.SaveMessage(message);
        }

        public async Task<TwilioStatusCallback> SaveStatusCallback(TwilioStatusCallback record)
        {
            record.CreateDate = DateTime.UtcNow;
            return await TwilioDataProvider.SaveStatusCallback(record);
        }
    }
}
