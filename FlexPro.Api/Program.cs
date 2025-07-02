using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Localization;
using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Infrastructure.Services;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Repositories;
using QuestPDF.Infrastructure;
using FluentValidation.AspNetCore;
using FluentValidation;
using FlexPro.Api.API.Middlewares;
using FlexPro.Api.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FlexPro.Api.API.Hubs;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Http(
        "https://s1300161.eu-nbg-2.betterstackdata.com",
        queueLimitBytes: null,
        textFormatter: new JsonFormatter())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

var supportedCultures = new[] { "pt-BR" };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
    SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList()
};

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllers()
    .AddDataAnnotationsLocalization()
    .AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.Converters.Add((new JsonStringEnumConverter()));
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "FlexPro API",
        Description = "Api para controlar ferramentas internas da Proauto Kimium.",
        Contact = new  OpenApiContact
        {
            Name = "MurilloHMS",
            Url = new Uri("https://murillohms.vercel.app")
        }
    });
});

var environment = builder.Environment;
var connectionStringName = environment.IsDevelopment() ? "TestConnectionString" : "ConnectionString";
var connectionString = builder.Configuration[connectionStringName] ?? throw new InvalidOperationException($"Connection string: {connectionStringName}");
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseLazyLoadingProxies().UseNpgsql(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Registros dos services email
builder.Services.AddTransient<IEmailService, EmailService>();
      builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("SMTP"));

//Registros dos services de autenticação
builder.Services.AddHttpContextAccessor();

// Registros dos services 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAbastecimentoRepository, AbastecimentoRepository>();
builder.Services.AddScoped<AbastecimentoService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<InformativoService>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IIcmsService, IcmsService>();
builder.Services.AddScoped<ICalculoTransportadoraService, CalculoTransportadoraService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IParceiroRepository, ParceiroRepository>();
builder.Services.AddScoped<IProdutoLojaRepository, ProdutoLojaRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Notification Hub
builder.Services.AddSignalR();

//Registros das licenças
QuestPDF.Settings.License = LicenseType.Community;

//serilog 
builder.Host.UseSerilog();

//authorization
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var token = builder.Configuration["JWT:Secret"];
var key = Encoding.UTF8.GetBytes(token);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:ISSUER"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:AUDIENCE"],
        ClockSkew = TimeSpan.FromMinutes(5),
        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role 
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            Log.Information("Token validado para usuario: {User}, Roles: {Roles}, Claims: {Claims}",
                context.Principal?.Identity?.Name,
                string.Join(", ", context.Principal?.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value) ?? Array.Empty<string>()),
                string.Join(", ", context.Principal?.Claims.Select(c => $"{c.Type}: {c.Value}") ?? Array.Empty<string>()));
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Log.Error("Falha na autenticacao: {Error} | Detalhes: {InnerException} | StackTrace: {StackTrace}",
                context.Exception.Message,
                context.Exception.InnerException?.Message ?? "Nenhum detalhe adicional",
                context.Exception.StackTrace);
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Log.Warning("Autenticacao falhou: {Error} | Descricao: {ErrorDescription} | Token presente: {TokenPresent} | Token: {Token} | User: {User}",
                context.Error ?? "Nenhum erro especifico",
                context.ErrorDescription ?? "Sem descriçao",
                context.Request.Headers.ContainsKey("Authorization"),
                context.Request.Headers["Authorization"].ToString(),
                context.HttpContext.User?.Identity?.Name ?? "Nenhum usuario");
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\": \"Nao autorizado\"}");
        },
        OnForbidden = context =>
        {
            Log.Warning("Acesso proibido (403)");
            context.Response.StatusCode = 403;
            return Task.CompletedTask;
        }
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

Log.Information("Iniciando FlexPro API...");

var app = builder.Build();
// Configure pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlexPro API V1");
        c.RoutePrefix = string.Empty;
    });
}

using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbcontext.Database.EnsureCreated();
}

app.UseRouting();

app.UseRequestLocalization(localizationOptions);
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<DebugAuthMiddleware>();
app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationHub>("/notificationHub").AllowAnonymous();
});
    
app.Run();

public partial class Program { }