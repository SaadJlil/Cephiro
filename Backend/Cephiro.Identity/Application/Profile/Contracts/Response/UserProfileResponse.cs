using Cephiro.Identity.Application.Shared.Contracts;

namespace Cephiro.Identity.Application.Profile.Contracts.Response;

public sealed record UserProfileResponse
{
    public Guid? Id { get; set; }
    public Uri? ImageUri { get; set; }
    public string? Description { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsError = false;
    public Error? Error { get; set; } = null;
}