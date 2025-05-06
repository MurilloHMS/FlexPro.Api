using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using FlexPro.Api.API.Middlewares;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Application.Validators.Cliente;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Infrastructure.Repositories;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Infrastructure.Services;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var environment = builder.Environment;

// ============================
// 🔐 Serilog
// ============================
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// ============================
// 🌍 Cultura e Localização
// ============================
var supportedCultures = new[] { "pt-BR" };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
    SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList()
};
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// ============================
// 🔧 Services do ASP.NET
// ============================
builder.Services.AddControllers()
    .AddDataAnnotationsLocalization()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// ============================
// 📦 Swagger
// ============================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ============================
// 🗃️ DbContext e Identity
// ============================
var connectionStringName = environment.IsDevelopment() ? "TestDatabase" : "DefaultConnection";
var connectionString = config.GetConnectionString(connectionStringName) 
                     ?? throw new InvalidOperationException($"Connection string {connectionStringName} not found");

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// ============================
// 🛠️ Serviços da Aplicação
// ============================
builder.Services.AddScoped<IAbastecimentoRepository, AbastecimentoRepository>();
builder.Services.AddScoped<AbastecimentoService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<InformativoService>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IIcmsService, IcmsService>();
builder.Services.AddScoped<ICalculoTransportadoraService, CalculoTransportadoraService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
builder.Services.AddHttpContextAccessor();

// ============================
// ✅ FluentValidation
// ============================
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateClienteDtoValidator>();

// ============================
// 🧠 MediatR / AutoMapper
// ============================
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ============================
// 📄 QuestPDF (licença community)
// ============================
QuestPDF.Settings.License = LicenseType.Community;

// ============================
// 🔐 Autenticação JWT
// ============================
var key = Encoding.UTF8.GetBytes(config["JwtSettings:Secret"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
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
            Log.Information("Token validado para {User}, Roles: {Roles}, Claims: {Claims}",
                context.Principal?.Identity?.Name,
                string.Join(", ", context.Principal?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? []),
                string.Join(", ", context.Principal?.Claims.Select(c => $"{c.Type}: {c.Value}") ?? [])
            );
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Log.Error("Erro de autenticação: {Error} - {StackTrace}",
                context.Exception.Message,
                context.Exception.StackTrace);
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Log.Warning("Desafio de autenticação falhou: {ErrorDescription}", context.ErrorDescription);
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\": \"Não autorizado\"}");
        },
        OnForbidden = context =>
        {
            Log.Warning("Acesso negado (403)");
            context.Response.StatusCode = 403;
            return Task.CompletedTask;
        }
    };
});

// ============================
// 🔓 Autorização
// ============================
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// ============================
// 🌐 CORS
// ============================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// ============================
// 🚀 Build e Pipeline
// ============================
var app = builder.Build();

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

app.UseRequestLocalization(localizationOptions);

app.UseCors("AllowAll");

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseAuthentication();
app.UseMiddleware<DebugAuthMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
