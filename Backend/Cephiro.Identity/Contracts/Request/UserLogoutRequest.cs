
using Cephiro.Identity.Domain.Enum;

namespace Cephiro.Identity.Contracts.Request;

public sealed record UserLogoutRequest
{
    public const Activity Status = Activity.DISCONNECTED;
    public required Guid Id { get; set; }
}