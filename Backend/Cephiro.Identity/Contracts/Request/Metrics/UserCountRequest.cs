namespace Cephiro.Identity.Contracts.Request.Metrics;

public sealed class UserCountRequest 
{
    private Guid _id = Guid.Empty;

    public Guid GetIdFromRequestHeaders()
    {
        return _id;
    }
}