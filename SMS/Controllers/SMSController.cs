using Service.Interfaces;
using Service.Models;
using System;
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
            try { 
            var result = await _SMSService.Send(request);
            return Created<Entity.MessageLog>(@"/{result.Id}",result);
            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> Get([FromUri] Guid id)
        {
            try
            {
                var result = await _SMSService.GetById(id);
                return Ok<Entity.MessageLog>(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
