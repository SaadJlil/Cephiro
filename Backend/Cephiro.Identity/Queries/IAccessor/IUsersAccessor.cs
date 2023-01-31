    using Cephiro.Identity.Contracts.Response;


namespace Cephiro.Identity.Queries.IAccessor; 

public interface IUserAccess
{
    //Profile
    public Task<IEnumerable<UserProfileResponse?>> GetAllUsersProfile(int page, CancellationToken cancellation);
    public Task<UserProfileResponse?> GetUserProfileById(Guid Id, CancellationToken cancellation);
    public Task<UserProfileResponse?> GetUserProfileByPhone(string PhoneNumber, CancellationToken cancellation);
    public Task<UserProfileResponse?> GetUserProfileByEmail(string Email, CancellationToken cancellation);


    //Info
    public Task<IEnumerable<UserInfoResponse?>> GetAllUsersInfo(int page, CancellationToken cancellation);
    public Task<UserInfoResponse?> GetUserInfoById(Guid Id, CancellationToken cancellation);
    public Task<UserInfoResponse?> GetUserInfoByPhone(string PhoneNumber, CancellationToken cancellation);
    public Task<UserInfoResponse?> GetUserInfoByEmail(string Email, CancellationToken cancellation);
}