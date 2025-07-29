using FlexPro.Domain.Models;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Services;

namespace FlexPro.Api.Extensions;

public static class EmailExtensions
{
    public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<EmailSettings>(config.GetSection("SMTP"));
        services.AddTransient<IEmailService, EmailService>();
        return services;
    }
}