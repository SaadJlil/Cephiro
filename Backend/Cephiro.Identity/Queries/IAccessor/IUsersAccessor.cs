using Cephiro.Identity.Queries.utils;

namespace Cephiro.Identity.Queries.IAccessor; 

public interface IUserAccess
{
    //Profile
    public Task<IEnumerable<UserProfileDto?>> GetAllUsersProfile(CancellationToken cancellation);
    public Task<UserProfileDto?> GetUserProfileById(Guid Id, CancellationToken cancellation);
    public Task<UserProfileDto?> GetUserProfileByPhone(string PhoneNumber, CancellationToken cancellation);
    public Task<UserProfileDto?> GetUserProfileByEmail(string Email, CancellationToken cancellation);


    //Info
    public Task<IEnumerable<UserIdentityInfoDto?>> GetAllUsersInfo(CancellationToken cancellation);
    public Task<UserIdentityInfoDto?> GetUserInfoById(Guid Id, CancellationToken cancellation);
    public Task<UserIdentityInfoDto?> GetUserInfoByPhone(string PhoneNumber, CancellationToken cancellation);
    public Task<UserIdentityInfoDto?> GetUserInfoByEmail(string Email, CancellationToken cancellation);
}