
using TaskOfAlifTech.Service.Exceptions;

namespace TaskOfAlifTech.Api.Middlewares
{
    public class AppExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<AppExceptionMiddleware> logger;
        public AppExceptionMiddleware(RequestDelegate next, ILogger<AppExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (AppException ex)
            {
                await HandleExceptionAsync(context, ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                // log
                logger.LogError(ex.ToString());

                await HandleExceptionAsync(context, 500, ex.Message);
            }
        }
        public async Task HandleExceptionAsync(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;
            await context.Response.WriteAsJsonAsync(new
            {
                Code = code,
                Message = message
            });
        }
    }
}
