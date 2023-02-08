using Cephiro.Identity.Application.Shared.Contracts;

namespace Cephiro.Identity.Application.Authentication.Contracts.Response;

public sealed class PasswordUpdateResponse
{
    public bool IsError { get; set; }
    public Error? Error { get; set; }
}