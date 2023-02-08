using Cephiro.Identity.Application.Shared.Contracts;

namespace Cephiro.Identity.Application.Profile.Contracts.Response;

public sealed class UsersListResponse
{
    public IEnumerable<UserInfoResponse>? Users { get; set; }
    public Error? Error { get; set; }
}