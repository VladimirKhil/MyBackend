namespace MyBackend.Service.Contract.Response;

/// <summary>
/// Defines a years response model.
/// </summary>
/// <param name="Years">Array of years.</param>
public sealed record YearsResponse(int[] Years);
