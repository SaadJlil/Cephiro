using Cephiro.Identity.Domain.Exceptions;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Queries.IAccessor;
using Cephiro.Identity.Infrastructure.Data;
using Cephiro.Identity.Queries.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Cephiro.Identity.Infrastructure.Accessor;

public sealed class UserAccess: IUserAccess 
{
    private readonly IdentityDbContext _db;

    public UserAccess(IdentityDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<GetAllUsersOutputDto>> GetAllUsersInfo()
    {
       return await _db.Users.OrderByDescending(x => x.RegularizationStage).Select(x => new GetAllUsersOutputDto(
            x.Id,
            x.FirstName,
            x.Email
       )).ToListAsync();
    }
    public async Task<GetUserByIdOutputDto?> GetUserInfoById(Guid Id)
    {
        return await _db.Users.Where(
            x => x.Id == Id
        ).Select(
            x => new GetUserByIdOutputDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }

    public async Task<GetUserByEmailOutputDto?> GetUserInfoByEmail(string Email)
    {
        return await _db.Users.Where(
            x => x.Email == Email
        ).Select(
            x => new GetUserByEmailOutputDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }
    public async Task<GetUserByPhoneOutputDto?> GetUserInfoByPhone(string PhoneNumber)
    {
        return await _db.Users.Where(
            x => x.PhoneNumber == PhoneNumber
        ).Select(
            x => new GetUserByPhoneOutputDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }





    //Profile
    public async Task<IEnumerable<GetAllUsersOutputDto>> GetAllUsersProfile()
    {
       return await _db.Users.OrderByDescending(x => x.RegularizationStage).Select(x => new GetAllUsersOutputDto(
            x.Id,
            x.FirstName,
            x.Email
       )).ToListAsync();
    }
    public async Task<GetUserByIdOutputDto?> GetUserProfileById(Guid Id)
    {
        return await _db.Users.Where(
            x => x.Id == Id
        ).Select(
            x => new GetUserByIdOutputDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }

    public async Task<GetUserByEmailOutputDto?> GetUserProfileByEmail(string Email)
    {
        return await _db.Users.Where(
            x => x.Email == Email
        ).Select(
            x => new GetUserByEmailOutputDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }
    public async Task<GetUserByPhoneOutputDto?> GetUserProfileByPhone(string PhoneNumber)
    {
        return await _db.Users.Where(
            x => x.PhoneNumber == PhoneNumber
        ).Select(
            x => new GetUserByPhoneOutputDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }

}
