namespace Cephiro.Identity.Contracts.Request.Profile;

public sealed class UserProfileRequest 
{
    private Guid _id = Guid.Empty;

    public Guid GetIdFromRequestHeaders()
    {
        return _id;
    }
}