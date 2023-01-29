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
    private IOptionsMonitor<DapperConfig> _settings;
    private readonly NpgsqlConnection _connect;

    public UserAccess(IdentityDbContext db, IOptionsMonitor<DapperConfig> settings)
    {
        _db = db;
        _settings = settings;
    }

    public async Task<IEnumerable<UserIdentityInfoDto?>> GetAllUsersInfo(CancellationToken cancellation)
    {
        IEnumerable<User> result;
        try{
            string sqlQuery = $@"SELECT * FROM users";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await _connect.QueryAsync<User>(query);
                db.Close();
            }

            return result.Select(x => new UserIdentityInfoDto(
                Id: x.Id,
                FirstName: x.FirstName,
                Email: x.Email
            ));
        }
        catch(NpgsqlException exception){

        }
    }
    public async Task<UserIdentityInfoDto?> GetUserInfoById(Guid Id, CancellationToken cancellation)
    {
        User result;
        try{
            string sqlQuery = $@"SELECT * FROM users WHERE id = @Id LIMIT 1";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Id },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await _connect.QueryFirstOrDefaultAsync<User>(query);
                db.Close();
            }

            return new UserIdentityInfoDto(
                Id: result.Id,
                FirstName: result.FirstName,
                Email: result.Email
            );
        }
        catch(NpgsqlException exception){

        }


    }

    public async Task<UserIdentityInfoDto?> GetUserInfoByEmail(string Email, CancellationToken cancellation)
    {
        User result;
        try{
            string sqlQuery = $@"SELECT * FROM users WHERE email = @Email LIMIT 1";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Email },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await _connect.QueryFirstOrDefaultAsync<User>(query);
                db.Close();
            }

            return new UserIdentityInfoDto(
                Id: result.Id,
                FirstName: result.FirstName,
                Email: result.Email
            );
        }
        catch(NpgsqlException exception){

        }


    }
    public async Task<UserIdentityInfoDto?> GetUserInfoByPhone(string PhoneNumber, CancellationToken cancellation)
    {
        User result;
        try{
            string sqlQuery = $@"SELECT * FROM users WHERE phone_number = @PhoneNumber LIMIT 1";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { PhoneNumber },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await _connect.QueryFirstOrDefaultAsync<User>(query);
                db.Close();
            }

            return new UserIdentityInfoDto(
                Id: result.Id,
                FirstName: result.FirstName,
                Email: result.Email
            );
        }
        catch(NpgsqlException exception){

        }


    }





    //Profile
    public async Task<IEnumerable<UserProfileDto?>> GetAllUsersProfile(CancellationToken cancellation)
    {
        
        IEnumerable<User> result;
        try{
            string sqlQuery = $@"SELECT * FROM users";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await _connect.QueryAsync<User>(query);
                db.Close();
            }

            return result.Select(x => new UserProfileDto(
                x.ImageUri,
                x.MiddleName != "" ? $"{x.FirstName} {x.MiddleName} {x.LastName}" : $"{x.FirstName} {x.LastName}",
                x.Email,
                x.PhoneNumber ?? "",
                x.Description ?? "",
                x.RegularizationStage
            ));

       }
        catch(NpgsqlException exception){

        }
    }
    public async Task<UserProfileDto?> GetUserProfileById(Guid Id, CancellationToken cancellation)
    {
        User result;
        try{
            string sqlQuery = $@"SELECT * FROM users WHERE id = @Id LIMIT 1";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Id },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await _connect.QueryFirstOrDefaultAsync<User>(query);
                db.Close();
            }

            return new UserProfileDto(
                result.ImageUri,
                result.MiddleName != "" ? $"{result.FirstName} {result.MiddleName} {result.LastName}" : $"{result.FirstName} {result.LastName}",
                result.Email,
                result.PhoneNumber ?? "",
                result.Description ?? "",
                result.RegularizationStage
            );
       }
        catch(NpgsqlException exception){

        }

   }

    public async Task<UserProfileDto?> GetUserProfileByEmail(string Email, CancellationToken cancellation)
    {
        string sqlQuery = $@"SELECT * FROM users WHERE email = @Email LIMIT 1";

        var query = new CommandDefinition(
            commandText: sqlQuery, 
            parameters: new { Email },
            cancellationToken: cancellation);

        var result = await _connect.QueryFirstOrDefaultAsync<User>(query);

        return new UserProfileDto(
            result.ImageUri,
            result.MiddleName != "" ? $"{result.FirstName} {result.MiddleName} {result.LastName}" : $"{result.FirstName} {result.LastName}",
            result.Email,
            result.PhoneNumber ?? "",
            result.Description ?? "",
            result.RegularizationStage
        );
    }
    public async Task<UserProfileDto?> GetUserProfileByPhone(string PhoneNumber, CancellationToken cancellation)
    {
        string sqlQuery = $@"SELECT * FROM users WHERE phone_number = @PhoneNumber LIMIT 1";


        var query = new CommandDefinition(
            commandText: sqlQuery, 
            parameters: new { PhoneNumber },
            cancellationToken: cancellation);

        var result = await _connect.QueryFirstOrDefaultAsync<User>(query);

        return new UserProfileDto(
            result.ImageUri,
            result.MiddleName != "" ? $"{result.FirstName} {result.MiddleName} {result.LastName}" : $"{result.FirstName} {result.LastName}",
            result.Email,
            result.PhoneNumber ?? "",
            result.Description ?? "",
            result.RegularizationStage
        );
    } 

}
