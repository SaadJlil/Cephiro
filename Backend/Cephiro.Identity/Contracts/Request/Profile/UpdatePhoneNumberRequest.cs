namespace Cephiro.Identity.Contracts.Request.Profile;

public sealed class UpdatePhoneNumberRequest
{
    private Guid _id = Guid.Empty;
    public string? OldPhoneNumber { get; set; }
    public required string NewPhoneNumber { get; set; }

    public void SetIdFromJwtHeader(Guid id)
    {
        _id = id;
    }

    public Guid GetIdFromRequestBody()
    {
        return _id;
    }
}
