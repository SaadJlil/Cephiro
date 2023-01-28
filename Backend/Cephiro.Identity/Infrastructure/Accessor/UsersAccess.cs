using Cephiro.Identity.Domain.Exceptions;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Queries.IAccessor;
using Cephiro.Identity.Infrastructure.Data;
using Cephiro.Identity.Queries.utils;
using Microsoft.EntityFrameworkCore;

namespace Cephiro.Identity.Infrastructure.Accessor;

public sealed class UserAccess: IUserAccess 
{
    private readonly IdentityDbContext _db;

    public UserAccess(IdentityDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<UserIdentityInfoDto?>> GetAllUsersInfo()
    {
       return await _db.Users.OrderByDescending(x => x.RegularizationStage).Select(
        x => new UserIdentityInfoDto(
            x.Id,
            x.FirstName,
            x.Email
       )).ToListAsync();
    }
    public async Task<UserIdentityInfoDto?> GetUserInfoById(Guid Id)
    {
        return await _db.Users.Where(
            x => x.Id == Id
        ).Select(
            x => new UserIdentityInfoDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }

    public async Task<UserIdentityInfoDto?> GetUserInfoByEmail(string Email)
    {
        return await _db.Users.Where(
            x => x.Email == Email
        ).Select(
            x => new UserIdentityInfoDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }
    public async Task<UserIdentityInfoDto?> GetUserInfoByPhone(string PhoneNumber)
    {
        return await _db.Users.Where(
            x => x.PhoneNumber == PhoneNumber
        ).Select(
            x => new UserIdentityInfoDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }





    //Profile
    public async Task<IEnumerable<UserProfileDto?>> GetAllUsersProfile()
    {
       return await _db.Users.OrderByDescending(x => x.RegularizationStage).Select(
        x => new UserProfileDto(
            x.ImageUri,
            x.MiddleName != "" ? $"{x.FirstName} {x.MiddleName} {x.LastName}" : $"{x.FirstName} {x.LastName}",
            x.Email,
            x.PhoneNumber ?? "",
            x.Description ?? "",
            x.RegularizationStage
       )).ToListAsync();
    }
    public async Task<UserProfileDto?> GetUserProfileById(Guid Id)
    {
        return await _db.Users.Where(
            x => x.Id == Id
        ).Select(
            x => new UserProfileDto(
                x.ImageUri,
                x.MiddleName != "" ? $"{x.FirstName} {x.MiddleName} {x.LastName}" : $"{x.FirstName} {x.LastName}",
                x.Email,
                x.PhoneNumber ?? "",
                x.Description ?? "",
                x.RegularizationStage

            )
        ).FirstOrDefaultAsync();
    }

    public async Task<UserProfileDto?> GetUserProfileByEmail(string Email)
    {
        return await _db.Users.Where(
            x => x.Email == Email
        ).Select(
            x => new UserProfileDto(
                x.ImageUri,
                x.MiddleName != "" ? $"{x.FirstName} {x.MiddleName} {x.LastName}" : $"{x.FirstName} {x.LastName}",
                x.Email,
                x.PhoneNumber ?? "",
                x.Description ?? "",
                x.RegularizationStage
            )
        ).FirstOrDefaultAsync();
    }
    public async Task<UserProfileDto?> GetUserProfileByPhone(string PhoneNumber)
    {
        return await _db.Users.Where(
            x => x.PhoneNumber == PhoneNumber
        ).Select(
            x => new UserProfileDto(
                x.ImageUri,
                x.MiddleName != "" ? $"{x.FirstName} {x.MiddleName} {x.LastName}" : $"{x.FirstName} {x.LastName}",
                x.Email,
                x.PhoneNumber ?? "",
                x.Description ?? "",
                x.RegularizationStage
            )
        ).FirstOrDefaultAsync();
    }

}
