﻿
using Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISMSService
    {
        Task<MessageLog> SaveLog(MessageLog log);
        Task<Entity.MessageLog> Send(Models.SendRequest request);
        Task<Entity.ThirdPartyService> FindThirdPartyService(string code);
        Task<Entity.MessageLog> GetById(Guid id);
        Task<List<Entity.MessageLogStatus>> ListMessageStatuses(Guid id);
        Task<List<Entity.MessageLogReply>> ListIncomingMessages();
    }
}
