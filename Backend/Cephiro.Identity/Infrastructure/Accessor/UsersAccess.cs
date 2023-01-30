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
    private IOptionsMonitor<DapperConfig> _settings;
    private string Profilesql = "image_uri, first_name, middle_name, last_name, phone_number, description, regularization_stage";
    private string Infosql = "id, first_name, email";

    public UserAccess(IdentityDbContext db, IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
    }

    public async Task<IEnumerable<UserIdentityInfoDto?>> GetAllUsersInfo(CancellationToken cancellation)
    {
        IEnumerable<User> result;
        try{
            string sqlQuery = $@"SELECT @Infosql FROM users";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Infosql },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryAsync<User>(query);
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
            string sqlQuery = $@"SELECT @Infosql FROM users WHERE id = @Id LIMIT 1";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Infosql, Id },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<User>(query);
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
            string sqlQuery = $@"SELECT @Infosql FROM users WHERE email = @Email LIMIT 1";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Infosql, Email },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<User>(query);
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
            string sqlQuery = $@"SELECT @Infosql FROM users WHERE phone_number = @PhoneNumber LIMIT 1";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Infosql, PhoneNumber },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<User>(query);
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
            string sqlQuery = $@"SELECT @Profilesql FROM users";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new {Profilesql},
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryAsync<User>(query);
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
            string sqlQuery = $@"SELECT @Profilesql FROM users WHERE id = @Id LIMIT 1";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Profilesql, Id },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<User>(query);
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
        User result;
        try{
            string sqlQuery = $@"SELECT @Profilesql FROM users WHERE email = @Email LIMIT 1";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Profilesql , Email },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<User>(query);
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
    public async Task<UserProfileDto?> GetUserProfileByPhone(string PhoneNumber, CancellationToken cancellation)
    {
        User result;
        try{
            string sqlQuery = $@"SELECT @Profilesql FROM users WHERE phone_number = @PhoneNumber LIMIT 1";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Profilesql, PhoneNumber },
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<User>(query);
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
}
