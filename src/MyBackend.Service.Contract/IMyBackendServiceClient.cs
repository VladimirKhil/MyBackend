namespace MyBackend.Service.Contract;

/// <summary>
/// Defines a MyBackend service client.
/// </summary>
public interface IMyBackendServiceClient
{
    /// <summary>
    /// Provides API for working with news.
    /// </summary>
    INewsApi News { get; }

    /// <summary>
    /// Provides admin level API.
    /// </summary>
    IAdminApi Admin { get; }
}
