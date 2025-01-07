using AspNetCoreRateLimit;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using Microsoft.Extensions.Options;
using MyBackend.Database;
using MyBackend.Service.Configuration;
using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contract.Response;
using MyBackend.Service.Contracts;
using MyBackend.Service.EndpointDefinitions;
using MyBackend.Service.Metrics;
using MyBackend.Service.Middlewares;
using MyBackend.Service.Services;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Serilog;
using System.Data.Common;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(new Serilog.Formatting.Display.MessageTemplateTextFormatter(
        "[{Timestamp:yyyy/MM/dd HH:mm:ss} {Level}] {Message:lj} {Exception}{NewLine}"))
    .ReadFrom.Configuration(ctx.Configuration)
    .Filter.ByExcluding(logEvent =>
        logEvent.Exception is BadHttpRequestException || logEvent.Exception is OperationCanceledException));

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();

Configure(app);

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.Configure<MyBackendOptions>(configuration.GetSection(MyBackendOptions.ConfigurationSectionName));

    services.AddTransient<INewsService, NewsService>();
    services.AddTransient<IBlogsService, BlogsService>();

    services.AddMyBackendDatabase(configuration);
    ConfigureMigrationRunner(services, configuration);

    AddRateLimits(services, configuration);
    AddMetrics(services);

    services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.TypeInfoResolverChain.Insert(0, MyBackendContext.Default);
    });
}

static void ConfigureMigrationRunner(IServiceCollection services, IConfiguration configuration)
{
    services.AddSingleton<IConventionSet>(new DefaultConventionSet(DbConstants.Schema, null));

    var dbConnectionString = configuration.GetConnectionString("MyBackend");

    services
        .AddFluentMigratorCore()
        .ConfigureRunner(migratorBuilder =>
            migratorBuilder
                .AddPostgres()
                .WithGlobalConnectionString(dbConnectionString)
                .ScanIn(typeof(DbConstants).Assembly).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole());
}

static void AddRateLimits(IServiceCollection services, IConfiguration configuration)
{
    services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimit"));

    services.AddMemoryCache();
    services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    services.AddInMemoryRateLimiting();
}

static void AddMetrics(IServiceCollection services)
{
    var meters = new OtelMetrics();

    services.AddOpenTelemetry().WithMetrics(builder =>
        builder
            .ConfigureResource(rb => rb.AddService("MyBackend"))
            .AddMeter(meters.MeterName)
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation()
            .AddPrometheusExporter());

    services.AddSingleton(meters);
}

static void Configure(WebApplication app)
{
    var options = app.Services.GetRequiredService<IOptions<MyBackendOptions>>().Value;

    app.UseMiddleware<ErrorHandlingMiddleware>();

    NewsEndpointDefinitions.DefineNewsEndpoint(app);
    BlogsEndpointDefinitions.DefineBlogsEndpoints(app);
    TagsEndpointDefinitions.DefineTagsEndpoints(app);
    AdminEndpointDefinitions.DefineAdminEndpoint(app);

    app.UseIpRateLimiting();

    CreateDatabase(app);
    ApplyMigrations(app);

    app.UseOpenTelemetryPrometheusScrapingEndpoint();
}

static void CreateDatabase(WebApplication app)
{
    var dbConnectionString = app.Configuration.GetConnectionString("MyBackend");

    var connectionStringBuilder = new DbConnectionStringBuilder
    {
        ConnectionString = dbConnectionString
    };

    connectionStringBuilder["Database"] = "postgres";

    DatabaseExtensions.EnsureExists(connectionStringBuilder.ConnectionString!, DbConstants.DbName);
}

static void ApplyMigrations(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

    if (runner.HasMigrationsToApplyUp())
    {
        runner.MigrateUp();
    }
}

[JsonSerializable(typeof(NewsItem))]
[JsonSerializable(typeof(NewsResponse))]
[JsonSerializable(typeof(int[]))]
[JsonSerializable(typeof(YearsResponse))]
internal partial class MyBackendContext : JsonSerializerContext { }
