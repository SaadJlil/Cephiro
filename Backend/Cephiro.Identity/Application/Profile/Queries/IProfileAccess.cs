using Cephiro.Identity.Application.Profile.Contracts.Response;

namespace Cephiro.Identity.Application.Profile.Queries;

public interface IProfileAccess
{
    public Task<UserProfileResponse> GetUserProfile(Guid id, CancellationToken token);
    public Task<UsersListResponse> GetUsers(int take, int skip, CancellationToken token);
    
}