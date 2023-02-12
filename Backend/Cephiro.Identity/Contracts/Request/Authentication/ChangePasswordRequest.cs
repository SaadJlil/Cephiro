using Cephiro.Identity.Domain.ValueObjects;

namespace Cephiro.Identity.Contracts.Request.Authentication;

public sealed record ChangePasswordRequest
{
    private Guid _id = Guid.Empty;
    public required Password OldPassword { get; set; }
    public required Password NewPassword { get; set; }

    public void SetIdFromJwtHeader(Guid id)
    {
        _id = id;
    }

    public Guid GetIdFromJwtHeader()
    {
        return _id;
    }
}