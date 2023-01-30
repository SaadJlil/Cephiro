namespace Cephiro.Identity.Contracts.Request.Profile;

public sealed class UpdateProfileBioRequest
{
    private Guid _id = Guid.Empty;
    public required string Description { get; set; }

    public void SetIdFromJwtHeader(Guid id)
    {
        _id = id;
    }

    public Guid GetIdFromRequestHeaders()
    {
        return _id;
    }
}