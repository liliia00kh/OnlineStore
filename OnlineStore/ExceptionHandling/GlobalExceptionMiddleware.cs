namespace OnlineStore.ExceptionHandling
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                _logger.LogInformation("Handling request: {Path}", context.Request.Path);
                await next(context);
                _logger.LogInformation("Finished request: {Path}", context.Request.Path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in middleware pipeline");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new { error = "Something went wrong at middleware level" };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
