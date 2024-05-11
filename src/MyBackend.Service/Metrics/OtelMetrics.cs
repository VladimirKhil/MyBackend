using System.Diagnostics.Metrics;

namespace MyBackend.Service.Metrics;

/// <summary>
/// Holds service metrics.
/// </summary>
public sealed class OtelMetrics
{
    public string MeterName { get; }

    public OtelMetrics(string meterName = "MyBackend")
    {
        var meter = new Meter(meterName);
        MeterName = meterName;

        // TODO: implement custom service metrics
    }
}
