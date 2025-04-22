using System.Text.Json;
using FluentValidation;

namespace FlexPro.Api.API.Middlewares
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ValidationExceptionMiddleware> _logger;

        public ValidationExceptionMiddleware(RequestDelegate next, ILogger<ValidationExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(FluentValidation.ValidationException ex)
            {
                _logger.LogWarning("Erro de validação: {Message}", ex.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { errors = ex.Errors });
            }
        }
    }
}
