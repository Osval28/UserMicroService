using Microsoft.AspNetCore.Diagnostics;
using UserService.Application.Exceptions;

namespace UserService.Infrastructure.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, message) = exception switch
            {
                BusinessException ex => (StatusCodes.Status400BadRequest, ex.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new { message }, cancellationToken);
            return true;
        }
    }
}