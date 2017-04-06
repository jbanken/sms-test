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
        public ISMSService SMSService { get; set; }

        public SMSController(ISMSService smsService)
        {
            SMSService = smsService;
        }

        [HttpPost]
        [Route("send")]
        public async Task<IHttpActionResult> Send([FromBody]SendRequest request)
        {
            try { 
            var result = await SMSService.Send(request);
            return Ok<Entity.MessageLog>(result);
            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
