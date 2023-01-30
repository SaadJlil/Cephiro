namespace Cephiro.Identity.Contracts.Response;

public sealed record HashedPasswordResponse
{
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}