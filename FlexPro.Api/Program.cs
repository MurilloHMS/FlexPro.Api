using System.Text.Json.Serialization;
using DotNetEnv;
using FlexPro.Api.API.Hubs;
using FlexPro.Api.API.Middlewares;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Extensions;
using FlexPro.Application;
using FlexPro.Infrastructure;
using FlexPro.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);
Env.Load();
builder.Configuration.AddEnvironmentVariables();

var config =  builder.Configuration;
var env = builder.Environment;
var connectionStringName = env.IsDevelopment() ? "TestConnectionString" : "ConnectionString";
var connectionString = config[connectionStringName] ?? throw new InvalidOperationException($"Connection string: {connectionStringName}");

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseLazyLoadingProxies().UseNpgsql(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//Registros dos services de autenticação
builder.Services.AddHttpContextAccessor();

// Registros dos services 
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddJwtAuthentication(config);
builder.Services.AddCorsPolicy();
builder.Services.AddSwaggerDocs();
builder.Services.AddEmailServices(config);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(FlexPro.Api.Program).Assembly));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<FlexPro.Api.Program>();
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = null;
});

// Notification Hub
builder.Services.AddSignalR();

//Registros das licenças
QuestPDF.Settings.License = LicenseType.Community;

//authorization
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// logs
builder.Host.UseSerilog((context, ServiceCollectionServiceExtensions, LoggerConfiguration) =>
{
    LoggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Http(requestUri: context.Configuration["SEQ_URL"] ?? "http://seq:5342",
            queueLimitBytes: null,
            textFormatter: new JsonFormatter()
        );
});


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<FlexPro.Api.Program>>();
logger.LogInformation("Starting application");

if (env.IsDevelopment())
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
        c.ConfigObject.AdditionalItems["tryItOutEnabled"] = false;
    });
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseRequestLocalization(LocalizationExtensions.GetLocalizationOptions());
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

if (builder.Environment.IsDevelopment())
{
    app.UseMiddleware<DebugAuthMiddleware>();
}

app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationHub>("/notificationHub")
        .RequireCors("AllowFrontend").AllowAnonymous();

    endpoints.MapMethods("{*path}", new[] { "OPTIONS" }, context =>
    {
        context.Response.StatusCode = 204;
        return Task.CompletedTask;
    });
});

app.Run();

namespace FlexPro.Api
{
    public partial class Program { }
}