using Service.Exceptions;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace API.Handlers
{
       
    public class UnhandledExceptionHandler : ExceptionHandler
    {
            
        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            if (context.Exception is NotFoundException)
            {
                context.Result = new ResponseMessageResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent(context.Exception.Message)
                });
            }
            else if (context.Exception is DbUpdateConcurrencyException)
            {
                context.Result = new ResponseMessageResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Content = null
                });
            }
            else
            {
                context.Result = new ResponseMessageResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(context.Exception.Message)
                });
           }

            await base.HandleAsync(context, cancellationToken);
        }
    }
}