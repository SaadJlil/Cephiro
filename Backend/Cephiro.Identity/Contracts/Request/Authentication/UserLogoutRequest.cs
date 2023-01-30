
using Cephiro.Identity.Domain.Enum;

namespace Cephiro.Identity.Contracts.Request.Authentication;

public sealed class UserLogoutRequest
{
    private Guid _id = Guid.Empty;
    public const Activity Status = Activity.DISCONNECTED;

    public void SetIdFromJwtHeader(Guid id)
    {
        _id = id;
    }

    public Guid GetIdFromJwtHeader()
    {
        return _id;
    }
}