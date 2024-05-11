namespace MyBackend.Service.Client;

/// <summary>
/// Provides options for <see cref="MyBackendServiceClient" /> class.
/// </summary>
internal sealed class MyBackendClientOptions
{
    /// <summary>
    /// Name of the configuration section holding these options.
    /// </summary>
    public const string ConfigurationSectionName = "MyBackendServiceClient";

    public const int DefaultRetryCount = 3;

    /// <summary>
    /// MyBackend service Uri.
    /// </summary>
    public Uri? ServiceUri { get; set; }

    /// <summary>
    /// Secret to access restricted API.
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// Retry count policy.
    /// </summary>
    public int RetryCount { get; set; } = DefaultRetryCount;

    /// <summary>
    /// Client timeout.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}
