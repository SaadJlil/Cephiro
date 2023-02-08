namespace Cephiro.Identity.Application.Authentication.Commands.Helpers;

public sealed class JwtConfig
{
    public const string SectionName = "JwtSettings";
    
    public string Secret { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
}