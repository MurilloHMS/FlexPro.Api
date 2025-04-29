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
using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Localization;
using FlexPro.Api.Infrastructure.Persistance;
using Serilog;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Infrastructure.Repositories;
using FlexPro.Api.Infrastructure.Services;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.API.Middlewares;
using FlexPro.Api.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;



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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found");
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

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


//Registros das licenças
QuestPDF.Settings.License = LicenseType.Community;

//serilog 
Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341")
    .WriteTo.Console()
    .CreateLogger();

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
            Log.Information("Token validado para usu�rio: {User}, Roles: {Roles}, Claims: {Claims}",
                context.Principal?.Identity?.Name,
                string.Join(", ", context.Principal?.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value) ?? Array.Empty<string>()),
                string.Join(", ", context.Principal?.Claims.Select(c => $"{c.Type}: {c.Value}") ?? Array.Empty<string>()));
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Log.Error("Falha na autentica��o: {Error} | Detalhes: {InnerException} | StackTrace: {StackTrace}",
                context.Exception.Message,
                context.Exception.InnerException?.Message ?? "Nenhum detalhe adicional",
                context.Exception.StackTrace);
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Log.Warning("Autentica��o falhou: {Error} | Descri��o: {ErrorDescription} | Token presente: {TokenPresent} | Token: {Token} | User: {User}",
                context.Error ?? "Nenhum erro espec�fico",
                context.ErrorDescription ?? "Sem descri��o",
                context.Request.Headers.ContainsKey("Authorization"),
                context.Request.Headers["Authorization"].ToString(),
                context.HttpContext.User?.Identity?.Name ?? "Nenhum usu�rio");
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\": \"N�o autorizado\"}");
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

var app = builder.Build();

/// Configure the HTTP request pipeline.
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
