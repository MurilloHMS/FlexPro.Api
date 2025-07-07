using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Services;

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