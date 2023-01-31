using Cephiro.Identity.Contracts.Response;
using ErrorOr;


namespace Cephiro.Identity.Queries.IAccessor; 

public interface IUserAccess
{
    //Profile
    public Task<ErrorOr<List<UserProfileResponse>>> GetAllUsersProfile(int page, CancellationToken cancellation);
    public Task<ErrorOr<UserProfileResponse>> GetUserProfileById(Guid Id, CancellationToken cancellation);
    public Task<ErrorOr<UserProfileResponse>> GetUserProfileByPhone(string PhoneNumber, CancellationToken cancellation);
    public Task<ErrorOr<UserProfileResponse>> GetUserProfileByEmail(string Email, CancellationToken cancellation);


    //Info
    public Task<ErrorOr<List<UserInfoResponse>>> GetAllUsersInfo(int page, CancellationToken cancellation);
    public Task<ErrorOr<UserInfoResponse>> GetUserInfoById(Guid Id, CancellationToken cancellation);
    public Task<ErrorOr<UserInfoResponse>> GetUserInfoByPhone(string PhoneNumber, CancellationToken cancellation);
    public Task<ErrorOr<UserInfoResponse>> GetUserInfoByEmail(string Email, CancellationToken cancellation);
}