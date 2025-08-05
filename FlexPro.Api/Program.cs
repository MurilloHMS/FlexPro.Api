using System.Text.Json.Serialization;
using DotNetEnv;
using FlexPro.Api.Extensions;
using FlexPro.Api.Hubs;
using FlexPro.Api.Middlewares;
using FlexPro.Application;
using FlexPro.Application.DTOs;
using FlexPro.Infrastructure;
using FlexPro.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF;
using QuestPDF.Infrastructure;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);
Env.Load();
builder.Configuration.AddEnvironmentVariables();

var config = builder.Configuration;
var env = builder.Environment;
var connectionStringName = env.IsDevelopment() ? "TestConnectionString" : "ConnectionString";
var connectionString = config[connectionStringName] ??
                       throw new InvalidOperationException($"Connection string: {connectionStringName}");

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
builder.Services.Configure<IISServerOptions>(options => { options.MaxRequestBodySize = null; });

// Notification Hub
builder.Services.AddSignalR();

//Registros das licenças
Settings.License = LicenseType.Community;

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
builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Http(context.Configuration["SEQ_URL"] ?? "http://seq:5342",
            null,
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
        c.SupportedSubmitMethods();
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

if (builder.Environment.IsDevelopment()) app.UseMiddleware<DebugAuthMiddleware>();

app.UseMiddleware<ValidationExceptionMiddleware>();

#pragma warning disable ASP0014
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
#pragma warning restore ASP0014

app.Run();

namespace FlexPro.Api
{
    public class Program
    {
    }
}