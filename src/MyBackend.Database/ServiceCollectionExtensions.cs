using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Data.RetryPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace MyBackend.Database;

/// <summary>
/// Provides a <see cref="IServiceCollection" /> extension that allows to register a MyBackend database.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds MyBackend database to the service collection.
    /// </summary>
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, typeof(MyBackendConnection))]
    public static void AddMyBackendDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        string connectionStringName = "MyBackend")
    {
        var dbConnectionString = configuration.GetConnectionString(connectionStringName)
            ?? throw new InvalidOperationException("Database connection string not found");
        
        services.AddLinqToDBContext<MyBackendConnection>((provider, options) =>
            options
                .UsePostgreSQL(dbConnectionString)
                .UseRetryPolicy(new TransientRetryPolicy())
                .UseDefaultLogging(provider));
    }
}
