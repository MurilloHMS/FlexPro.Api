using System.Globalization;
using System.Net.Http.Headers;
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
using MediatR;
using FlexPro.Api.API.Middlewares;
using FlexPro.Api.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Serilog;
using Serilog.Events;
using System.Net.Http.Headers;
using System.Reflection;
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
    options.JsonSerializerOptions.Converters.Add((new JsonStringEnumConverter()));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var environment = builder.Environment;
var connectionStringName = environment.IsDevelopment() ? "TestDatabase" : "DefaultConnection";
var connectionString = builder.Configuration.GetConnectionString(connectionStringName) ?? throw new InvalidOperationException($"Connection string: {connectionStringName}");
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Registros dos services email
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

//Registros dos services de autenticação
builder.Services.AddHttpContextAccessor();

// Registros dos services 
builder.Services.AddScoped<IAbastecimentoRepository, AbastecimentoRepository>();
builder.Services.AddScoped<AbastecimentoService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<InformativoService>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IIcmsService, IcmsService>();
builder.Services.AddScoped<ICalculoTransportadoraService, CalculoTransportadoraService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


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

var key = Encoding.UTF8.GetBytes(config["JwtSettings:Secret"]);
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
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = config["JwtSettings:Audience"],
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
    options.AddPolicy("AllowAll",
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

app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseRequestLocalization(localizationOptions);

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseMiddleware<DebugAuthMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }