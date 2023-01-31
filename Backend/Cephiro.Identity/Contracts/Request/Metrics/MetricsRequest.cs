namespace Cephiro.Identity.Contracts.Request.Metrics;

public sealed class MetricsRequest 
{
    private Guid _id = Guid.Empty;

    public Guid GetIdFromRequestHeaders()
    {
        return _id;
    }
}