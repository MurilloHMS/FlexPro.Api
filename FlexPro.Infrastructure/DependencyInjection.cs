using FlexPro.Application.Services;
using FlexPro.Domain.Entities;
using FlexPro.Domain.Interfaces;
using FlexPro.Domain.Interfaces.Pdf;
using FlexPro.Domain.Repositories;
using FlexPro.Infrastructure.Repositories;
using FlexPro.Infrastructure.Services;
using FlexPro.Infrastructure.Services.Pdf;
using Microsoft.Extensions.DependencyInjection;

namespace FlexPro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAbastecimentoRepository, AbastecimentoRepository>();
        services.AddScoped<AbastecimentoService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<InformativoService>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IJwtTokenGenerator<ApplicationUser>, JwtTokenGenerator>();
        services.AddScoped<IIcmsService, IcmsService>();
        services.AddScoped<ICalculoTransportadoraService, CalculoTransportadoraService>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IContatoRepository, ContatoRepository>();
        services.AddScoped<IParceiroRepository, ParceiroRepository>();
        services.AddScoped<IProdutoLojaRepository, ProdutoLojaRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IComputadorRepository, ComputadorRepository>();
        services.AddScoped<InventoryRepository>();
        services.AddScoped<InventoryService>();
        services.AddSingleton<IPdfReader, PdfReaderService>();
        services.AddSingleton<INameExtractor, NameExtractorService>();
        services.AddSingleton<IPdfWriter, PdfWriterService>();
        services.AddSingleton<IFileNameSanitizer, FileNameSanitizerService>();
        services.AddSingleton<IFileStorageService, FileStorageService>();
        services.AddSingleton<IPdfProcessing, PdfProcessingService>();
        services.AddSingleton<IFileNameSanitizer, FileNameSanitizerService>();


        return services;
    }
}