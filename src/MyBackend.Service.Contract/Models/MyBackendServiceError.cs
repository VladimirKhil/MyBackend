namespace MyBackend.Service.Contract.Models;

/// <summary>
/// Defines a MyBackend service error.
/// </summary>
public sealed class MyBackendServiceError
{
    /// <summary>
    /// Error code.
    /// </summary>
    public WellKnownMyBackendServiceErrorCode ErrorCode { get; set; }
}
