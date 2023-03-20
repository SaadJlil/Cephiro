using Cephiro.Identity.Application.Shared.Contracts;

namespace Cephiro.Identity.Application.Authentication.Contracts.Internal;
public struct HashedPasswordInternal
{
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public Error? Error { get; set; }
}