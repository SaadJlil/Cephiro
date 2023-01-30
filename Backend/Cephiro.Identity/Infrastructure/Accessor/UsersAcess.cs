using Cephiro.Identity.Domain.Exceptions;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Queries.IAccess;
using Cephiro.Identity.Infrastructure.Data;
using Cephiro.Identity.Queries.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Cephiro.Identity.Infrastructure.Access;

public sealed class UserAccess: IUserAccess 
{
    private readonly IdentityDbContext _db;

    public UserAccess(IdentityDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<GetAllUsersOutputDto>> GetAllUsers()
    {
       return await _db.Users.OrderByDescending(x => x.RegularizationStage).Select(x => new GetAllUsersOutputDto(
            x.Id,
            x.FirstName,
            x.Email
       )).ToListAsync();
    }
    public async Task<GetUserByIdOutputDto?> GetUserById(GetUserByIdInputDto Dto)
    {
        return await _db.Users.Where(
            x => x.Id == Dto.Id
        ).Select(
            x => new GetUserByIdOutputDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }

    public async Task<GetUserByEmailOutputDto?> GetUserByEmail(GetUserByEmailInputDto Dto)
    {
        return await _db.Users.Where(
            x => x.Email == Dto.Email
        ).Select(
            x => new GetUserByEmailOutputDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }
    public async Task<GetUserByPhoneOutputDto?> GetUserByPhone(GetUserByPhoneInputDto Dto)
    {
        return await _db.Users.Where(
            x => x.PhoneNumber == Dto.PhoneNumber
        ).Select(
            x => new GetUserByPhoneOutputDto(
                x.Id,
                x.FirstName,
                x.Email
            )
        ).FirstOrDefaultAsync();
    }
    
}
