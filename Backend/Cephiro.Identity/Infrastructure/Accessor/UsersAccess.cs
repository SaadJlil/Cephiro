using Cephiro.Identity.Domain.Exceptions;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Queries.IAccessor;
using Cephiro.Identity.Infrastructure.Data;
using Cephiro.Identity.Queries.utils;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Options;




namespace Cephiro.Identity.Infrastructure.Accessor;

public sealed class UserAccess: IUserAccess 
{
    private readonly IdentityDbContext _db;
    private readonly NpgsqlConnection _connect;

    public UserAccess(IdentityDbContext db, IOptionsMonitor<DapperConfig> settings)
    {
        _db = db;
        _connect = new NpgsqlConnection(settings.CurrentValue.IdentityConnection);
        _connect.Open();

    }

    public async Task<IEnumerable<UserIdentityInfoDto?>> GetAllUsersInfo(CancellationToken cancellation)
    {
        string sqlQuery = $@"SELECT * FROM users";

        var query = new CommandDefinition(
            commandText: sqlQuery, 
            cancellationToken: cancellation);

        var result = await _connect.QueryAsync<User>(query);

        return (IEnumerable<UserIdentityInfoDto?>)result;
    }
    public async Task<UserIdentityInfoDto?> GetUserInfoById(Guid Id, CancellationToken cancellation)
    {

        string sqlQuery = $@"SELECT * FROM users WHERE id = @Id LIMIT 1";

        var query = new CommandDefinition(
            commandText: sqlQuery, 
            parameters: new { Id },
            cancellationToken: cancellation);

        var result = await _connect.QueryFirstOrDefaultAsync<User>(query);

        return new UserIdentityInfoDto(
            Id: result.Id,
            FirstName: result.FirstName,
            Email: result.Email
        );
    }

    public async Task<UserIdentityInfoDto?> GetUserInfoByEmail(string Email, CancellationToken cancellation)
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
    public async Task<UserIdentityInfoDto?> GetUserInfoByPhone(string PhoneNumber, CancellationToken cancellation)
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
    public async Task<IEnumerable<UserProfileDto?>> GetAllUsersProfile(CancellationToken cancellation)
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
    public async Task<UserProfileDto?> GetUserProfileById(Guid Id, CancellationToken cancellation)
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

    public async Task<UserProfileDto?> GetUserProfileByEmail(string Email, CancellationToken cancellation)
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
    public async Task<UserProfileDto?> GetUserProfileByPhone(string PhoneNumber, CancellationToken cancellation)
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
