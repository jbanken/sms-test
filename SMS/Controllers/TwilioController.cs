using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Service.Interfaces;
namespace API.Controllers
{
    [RoutePrefix("twilio")]
    public class TwilioController : ApiController
    {
        public ITwilioService TwilioService { get; set; }

        public TwilioController(ITwilioService twilioService)
        {
            TwilioService = twilioService;
        }
        [HttpPost]
        [Route("status-callback")]
        /// <summary>
        /// Twilio - Receive status updates from messages. This end point is setup per message when sending. 
        /// </summary>
        public async Task<IHttpActionResult> StatusCallback([FromBody]Entity.TwilioStatusCallback request)
        {
            if (IsTwilioCallBack())
            {
                try {
                    await TwilioService.SaveStatusCallback(request);
                }catch(Exception ex)
                {
                    return InternalServerError(ex);
                }
                return Ok("Completed");
            }

            return BadRequest("Invalid Twilio Callback Request");
        }

        [HttpPost]
        [Route("incoming-message-callback")]
        /// <summary>
        /// Twilio - Receive replies from messages. This end point is setup inside Twilio Admin.
        /// </summary>
        public async Task<IHttpActionResult> IncomingMessageCallback([FromBody]Entity.TwilioIncomingMessageCallback request)
        {
            if (IsTwilioCallBack())
            {
                try
                {
                    await TwilioService.SaveIncomingMessageCallback(request);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
                return Ok("Completed");
            }

            return BadRequest("Invalid Twilio Callback Request");
        }

        private bool IsTwilioCallBack()
        {
            var userAgent = Request.Headers.GetValues("user-agent").FirstOrDefault();
            var twilioSignature = Request.Headers.GetValues("X-Twilio-Signature").FirstOrDefault();

            return (userAgent != null && userAgent.ToLower().StartsWith("twilioproxy") && twilioSignature != null);
        }
        
    }
}
