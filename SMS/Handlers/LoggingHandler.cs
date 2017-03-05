using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Service.Interfaces;
using System.ServiceModel.Channels;
using Microsoft.Owin;
using Logger.Interfaces;

namespace API.Handlers
{
    public class LoggingHandler : DelegatingHandler
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public ILogService LogService { get; set; }

        public LoggingHandler(ILogService logService)
        {
            LogService = logService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            //Log request
            var requestHeaders = request.Headers.ToDictionary(h => h.Key, h => h.Value);
            string headersToLog = String.Join(";", requestHeaders.Select(h => h.Key + ": " + String.Join(",", h.Value)));
            string requestBody = await request.Content.ReadAsStringAsync();

            var log = new Entity.APILog();
            log.GroupKey = Guid.NewGuid();
            log.RequestContentBody = requestBody;
            log.RequestContentType = request.Content.Headers.ContentType!= null ? request.Content.Headers.ContentType.ToString() : null;
            log.RequestHeaders = headersToLog;
            log.RequestIpAddress = GetClientIp(request);
            log.RequestMethod = request.Method.ToString();
            log.RequestTimeStamp = DateTime.UtcNow;
            log.RequestUri = request.RequestUri.ToString();
            log = await LogService.LogRequest(log);

            //set the APILogGroupKey in the owinContext for the log record GroupKey value for 3rd party calls so we know what external calls were made in this request
            var owinContext = request.GetOwinContext();
            owinContext.Set<Guid>("APILogGroupKey", (Guid)log.GroupKey);
            request.SetOwinContext(owinContext);

            //Response comes back
            var response = await base.SendAsync(request, cancellationToken);

            //Log response
            if (response.Content != null)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
                var responseHeaders = request.Headers.ToDictionary(h => h.Key, h => h.Value);

                log.ResponseContentBody = responseMessage;
                log.ResponseContentType = response.Content.Headers.ContentType.ToString();
                log.ResponseHeaders = String.Join("rn", responseHeaders.Select(h => h.Key + ": " + String.Join(",", h.Value)));
                log.ResponseStatusCode = (int?)response.StatusCode;
                log.ResponseTimeStamp = DateTime.UtcNow;

                await LogService.LogResponse(log);
            }

            //Return response
            return response;
        }

        private string GetClientIp(HttpRequestMessage request)
        {
            // Web-hosting
            if (request.Properties.ContainsKey(HttpContext))
            {
                HttpContextWrapper ctx =
                    (HttpContextWrapper)request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                RemoteEndpointMessageProperty remoteEndpoint =
                    (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin
            if (request.Properties.ContainsKey(OwinContext))
            {
                OwinContext owinContext = (OwinContext)request.Properties[OwinContext];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }

            return null;
        }

    }
}