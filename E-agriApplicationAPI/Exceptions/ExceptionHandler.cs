using System.Net;

namespace EcommerceApplicationAPI.Exceptions
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> logger;
        private readonly RequestDelegate next;

        public ExceptionHandler(ILogger<ExceptionHandler> logger,RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                logger.LogInformation("Method invoked");
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                logger.LogError(ex,$"{errorId} : {ex.Message}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong!"
                };
                await httpContext.Response.WriteAsJsonAsync(error);
               
            }
        }
      
    }
}
