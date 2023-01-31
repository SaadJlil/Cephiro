namespace Cephiro.Identity.Contracts.Request.Info;

public sealed class UserInfoRequest 
{
    private Guid _id = Guid.Empty;

    public Guid GetIdFromRequestHeaders()
    {
        return _id;
    }
}