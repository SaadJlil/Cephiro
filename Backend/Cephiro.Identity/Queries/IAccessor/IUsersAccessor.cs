using Cephiro.Identity.Queries.Dtos;
using Cephiro.Identity.Domain.Entities; 

namespace Cephiro.Identity.Queries.IAccessor; 

public interface IUserAccess
{

    //Profile
    public Task<IEnumerable<UserProfileDto?>> GetAllUsersProfile();
    public Task<UserProfileDto?> GetUserProfileById(Guid Id);
    public Task<UserProfileDto?> GetUserByProfilePhone(string PhoneNumber);
    public Task<UserProfileDto?> GetUserByProfileEmail(string Email);


    //Info
    public Task<IEnumerable<UserIdentityInfoDto?>> GetAllUsersInfo();
    public Task<UserIdentityInfoDto?> GetUserInfoById(Guid Id);
    public Task<UserIdentityInfoDto?> GetUserByInfoPhone(string PhoneNumber);
    public Task<UserIdentityInfoDto?> GetUserByInfoEmail(string Email);


}