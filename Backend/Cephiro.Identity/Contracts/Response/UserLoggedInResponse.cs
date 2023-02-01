using ErrorOr;

namespace Cephiro.Identity.Contracts.Response;

public sealed record UserLoggedInResponse
{
    public required ErrorOr<string> Jwt { get; set; }
}