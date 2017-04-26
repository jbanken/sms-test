using System;
using System.Threading.Tasks;
using Entity;
using DataProvider.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;

namespace Service
{
    public class SMSService : Interfaces.ISMSService
    {
        private ISMSDataProvider _SMSDataProvider;
        private ITwilioService _TwilioService;

        public SMSService(ISMSDataProvider smsDataProvider, ITwilioService twilioService)
        {
            _SMSDataProvider = smsDataProvider;
            _TwilioService = twilioService;
        }

        public async Task<ThirdPartyService> FindThirdPartyService(string code)
        {
            return await _SMSDataProvider.FindThirdPartyService(code);
        }

        public async Task<MessageLog> SaveLog(MessageLog log)
        {
            return await _SMSDataProvider.SaveLog(log);
        }

        public async Task<MessageLog> Send(Models.SendRequest request)
        {
            //log message
            var log = new Entity.MessageLog();
            log.To = request.To;
            log.From = request.From;
            log.Body = request.Message;
            log.ReferenceCode = request.ReferenceCode;
            log.ThirdPartyServiceID = Guid.Parse("CE38FF59-A125-406B-81DB-FBF25BB06331");//TODO fix
            log = await SaveLog(log);
            request.MessageId = log.Id;

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
            var sendResponse = await _TwilioService.Send(request);

            var log = await GetById((Guid)request.MessageId);

            if(log== null)
            {
                throw new Exceptions.NotFoundException();
            }
            log.ThirdPartyReferenceCode = sendResponse.Sid;
            log = await SaveLog(log);
        }

        public async Task<Entity.MessageLog> GetById(Guid id)
        {
            var log = await _SMSDataProvider.GetById(id);
            if (log == null)
            {
                throw new Exceptions.NotFoundException();
            }
            return log;
        }

        public async Task<List<Entity.MessageLogStatus>> ListMessageStatuses(Guid id)
        {
            return await _SMSDataProvider.ListMessageStatuses(id);
        }

        public async Task<List<Entity.MessageLogReply>> ListIncomingMessages()
        {
            return await _SMSDataProvider.ListIncomingMessages();
        }
    }
}
