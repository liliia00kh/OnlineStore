using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Exceptions;

namespace OnlineStore.ExceptionHandling
{
    public class ProductExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ProductExceptionFilter> _logger;

        public ProductExceptionFilter(ILogger<ProductExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Exception caught in API filter");

            var (statusCode, errorMessage) = context.Exception switch
            {
                ProductNotFoundException => (StatusCodes.Status404NotFound, context.Exception.Message),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized access"),
                _ => (StatusCodes.Status500InternalServerError, "Something went wrong in Product API")
            };

            var errorResponse = new
            {
                error = errorMessage,
                details = context.Exception.Message
            };

            context.Result = new JsonResult(errorResponse)
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }

}
