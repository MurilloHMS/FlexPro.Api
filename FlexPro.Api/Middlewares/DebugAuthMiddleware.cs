using Serilog;

namespace FlexPro.Api.Middlewares;

public class DebugAuthMiddleware
{
    private readonly RequestDelegate _next;

    public DebugAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Log.Information("Antes da autorização - Usuário: {User}, Autenticado: {IsAuthenticated}",
            context.User.Identity?.Name ?? "Nenhum",
            context.User.Identity?.IsAuthenticated ?? false);
        await _next(context);
    }
}