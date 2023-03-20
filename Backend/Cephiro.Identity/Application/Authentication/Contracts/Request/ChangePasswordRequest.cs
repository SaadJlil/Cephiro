using Cephiro.Identity.Domain.Attributes;

namespace Cephiro.Identity.Application.Authentication.Contracts.Request;

public sealed record ChangePasswordRequest
{
    private Guid _id = Guid.Empty;
    [Password] public required string OldPassword { get; set; }
    [Password] public required string NewPassword { get; set; }

    public void SetIdFromJwtHeader(Guid id)
    {
        _id = id;
    }

    public Guid GetIdFromJwtHeader()
    {
        return _id;
    }
}