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
        public ITwilioService TwilioService { get; set; }

        public SMSService(ISMSDataProvider smsDataProvider, ITwilioService twilioService)
        {
            SMSDataProvider = smsDataProvider;
            TwilioService = twilioService;
        }

        public async Task<ThirdPartyService> FindThirdPartyService(string code)
        {
            return await SMSDataProvider.FindThirdPartyService(code);
        }

        public async Task<MessageLog> SaveLog(MessageLog log)
        {
            return await SMSDataProvider.SaveLog(log);
        }

        public async Task<MessageLog> Send(Models.SendRequest request)
        {
            //log message
            var log = new Entity.MessageLog();
            log.To = request.To;
            log.From = request.From;
            log.ReferenceCode = request.ReferenceCode;
            log.ThirdPartyServiceID = 1;
            log = await SaveLog(log);

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
            var sendResponse = await TwilioService.Send(request);
        }
    }
}
