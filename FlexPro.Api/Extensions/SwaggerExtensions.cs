using Microsoft.OpenApi.Models;

namespace FlexPro.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "FlexPro API",
                Description = "API para ferramentas internas da Proauto Kimium.",
                Contact = new OpenApiContact
                {
                    Name = "MurilloHMS",
                    Url = new Uri("https://murillohms.vercel.app")
                }
            });
        });

        return services;
    }
}