using FluentValidation;

namespace FlexPro.Api.Middlewares;

public class ValidationExceptionMiddleware
{
    private readonly ILogger<ValidationExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

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
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Erro de validação: {Message}", ex.Message);
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { errors = ex.Errors });
        }
    }
}