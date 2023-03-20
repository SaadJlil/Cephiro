namespace Cephiro.Identity.Application.Profile.Contracts.Request;

public sealed class UpdateProfileImageRequest
{
    private Guid _id = Guid.Empty;
    public required Uri ImageUri { get; set; }

    public void SetIdFromJwtHeader(Guid id)
    {
        _id = id;
    }

    public Guid GetIdFromRequestHeaders()
    {
        return _id;
    }
}