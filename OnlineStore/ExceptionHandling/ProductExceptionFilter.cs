using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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

            var errorResponse = new
            {
                error = "Something went wrong in Product API",
                details = context.Exception.Message
            };

            context.Result = new JsonResult(errorResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }

}
