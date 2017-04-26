using Entity;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("sms")]
    public class SMSController : ApiController
    {
        private ISMSService _SMSService;

        public SMSController(ISMSService smsService)
        {
            _SMSService = smsService;
        }

        [HttpPost]
        [Route("send")]
        public async Task<IHttpActionResult> Send([FromBody]SendRequest request)
        {
            //TODO remove, used for testing
            request.From = string.IsNullOrEmpty(request.From) ? System.Configuration.ConfigurationManager.AppSettings["TwilioTestFrom"]: request.From;
            var result = await _SMSService.Send(request);
            return Created<Entity.MessageLog>(@"/{result.Id}",result);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get([FromUri] Guid id)
        {
            var result = await _SMSService.GetById(id);
            return Ok<Entity.MessageLog>(result);
        }

        [HttpGet]
        [Route("{id:guid}/statuses")]
        public async Task<IHttpActionResult> Statuses([FromUri] Guid id)
        {
            var results = await _SMSService.ListMessageStatuses(id);
            return Ok<List<MessageLogStatus>>(results);
        }

        [HttpGet]
        [Route("received-messages")]
        public async Task<IHttpActionResult> ReceivedMesssages()
        {
            var results = await _SMSService.ListIncomingMessages();
            return Ok<List<MessageLogReply>>(results);
        }
    }
}
