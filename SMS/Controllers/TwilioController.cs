using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("twilio")]
    public class TwilioController : ApiController
    {
        [HttpPost]
        [Route("webhook")]
        public async Task<IHttpActionResult> Send([FromBody]Models.Proxy.TwilioWebhookProxy request)
        {
            return Ok("Completed");
        }
    }
}
