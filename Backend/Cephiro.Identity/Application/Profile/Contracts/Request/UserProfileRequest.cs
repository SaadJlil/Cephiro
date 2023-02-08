namespace Cephiro.Identity.Application.Profile.Contracts.Request;

public sealed class UserProfileRequest
{
    public required Guid userId { get; set; }
}