using Cephiro.Identity.Application.Shared.Contracts;

namespace Cephiro.Identity.Application.Profile.Contracts.Response;

public sealed record UserInfoResponse
{
    public string? Email { get; set; }
    public string? Phone { get; set;}
    public string? FirstName { get; set;}
    public string? LastName { get; set;}
    public bool IsError { get; set; }
    public Error? Error { get; set; }
}