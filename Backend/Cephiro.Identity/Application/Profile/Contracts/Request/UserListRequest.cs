namespace Cephiro.Identity.Application.Profile.Contracts.Request;

public sealed class UserListRequest
{
    private Guid _id = Guid.Empty;

    public void SetIdFromJwtHeader(Guid id)
    {
        _id = id;
    }

    public Guid GetIdFromRequestHeaders()
    {
        return _id;
    }

    public int Take { get; set; }
    public int Skip { get; set; }
}