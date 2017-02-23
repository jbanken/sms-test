using System;
using System.Threading.Tasks;
using Entity;
using DataProvider.Interfaces;
using Service.Interfaces;
using SMSProvider.Interfaces;
namespace Service
{
    public class SMSService : Interfaces.ISMSService
    {
        public ISMSDataProvider SMSDataProvider { get; set; }
        public ITwilioSMSProvider TwilioSMSProvider { get; set; }
        public SMSService(ISMSDataProvider smsDataProvider, ITwilioSMSProvider twilioSMSProvider)
        {
            SMSDataProvider = smsDataProvider;
            TwilioSMSProvider = twilioSMSProvider;
        }

        public async Task<ThirdPartyService> FindThirdPartyService(string code)
        {
            return await SMSDataProvider.FindThirdPartyService(code);
        }

        public async Task<MessageLog> Send(Models.SendRequest request)
        {
            var log = new MessageLog();
            log.CreateDate = DateTime.UtcNow;
            log.ReferenceCode = request.ReferenceCode;
            log.To = request.To;
            log.From = request.From;
            log = await SMSDataProvider.Log(log);

            //TODO save the log body(s) any message over a 160 chars will be split into multiple messages

            //TODO actually have a queueing mechnamism 
            await EnQueue(request);

            return log;

        }

        private async Task EnQueue(Models.SendRequest request)
        {
            var sendRequest = new SMSProvider.Models.SendRequest();
            sendRequest.To = request.To;
            sendRequest.From = request.From;
            sendRequest.Message = request.Message;
            sendRequest.ReferenceCode = request.ReferenceCode;

            //TODO allow for SMSProviders to be swapped out
            var sendResponse = await TwilioSMSProvider.Send(sendRequest);
        }
    }
}
