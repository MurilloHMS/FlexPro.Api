using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Infrastructure.Repositories;
using FlexPro.Api.Infrastructure.Services;

namespace FlexPro.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAbastecimentoRepository, AbastecimentoRepository>();
        services.AddScoped<AbastecimentoService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<InformativoService>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IIcmsService, IcmsService>();
        services.AddScoped<ICalculoTransportadoraService, CalculoTransportadoraService>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IContatoRepository, ContatoRepository>();
        services.AddScoped<IParceiroRepository, ParceiroRepository>();
        services.AddScoped<IProdutoLojaRepository, ProdutoLojaRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IComputadorRepository, ComputadorRepository>();

        return services;
    }
}