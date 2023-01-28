using Cephiro.Identity.Queries.Dtos;

namespace Cephiro.Identity.Queries.IAccessor; 

public interface IUserAccess
{
    //Profile
    public Task<IEnumerable<UserProfileDto?>> GetAllUsersProfile();
    public Task<UserProfileDto?> GetUserProfileById(Guid Id);
    public Task<UserProfileDto?> GetUserProfileByPhone(string PhoneNumber);
    public Task<UserProfileDto?> GetUserProfileByEmail(string Email);


    //Info
    public Task<IEnumerable<UserIdentityInfoDto?>> GetAllUsersInfo();
    public Task<UserIdentityInfoDto?> GetUserInfoById(Guid Id);
    public Task<UserIdentityInfoDto?> GetUserInfoByPhone(string PhoneNumber);
    public Task<UserIdentityInfoDto?> GetUserInfoByEmail(string Email);
}