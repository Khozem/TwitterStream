using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace TwitterStreamExceptionHandling
{
    public class ServiceExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<ServiceExceptionFilter> _logger;
        public ServiceExceptionFilter(ILogger<ServiceExceptionFilter> logger)
        {
            _logger = logger;

        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError($"Something went wrong: {context.Exception}");
            await HandleExceptionAsync(context, context.Exception);
        }

        private Task HandleExceptionAsync(ExceptionContext context, Exception exception)
        {

            if (context.Exception is IValidationException)
            {
                context.Result = new ObjectResult(new ErrorDetails()
                {
                    Message  =  string.IsNullOrEmpty(context.Exception.Message) ? ((IValidationException)context.Exception).DefaultMessage : context.Exception.Message,
                })
                {
                    StatusCode = (int)((IValidationException)context.Exception).StatusCode,
                    ContentTypes = new MediaTypeCollection() { "application/json" }
                };
            }
            else
            {

                context.Result = new ObjectResult(new ErrorDetails()
                {
                    Message = context.Exception.Message,
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ContentTypes = new MediaTypeCollection() { "application/json" }
                };
            }

            return Task.CompletedTask;
        }
    }
}
