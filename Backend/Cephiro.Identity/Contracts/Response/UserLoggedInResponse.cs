namespace Cephiro.Identity.Contracts.Response;

public sealed record UserLoggedInResponse
{
    public required string Jwt { get; set; }
}