using Cephiro.Identity.Domain.Exceptions;
using Cephiro.Identity.Domain.Entities;
<<<<<<< HEAD
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
    private int rows_per_page;

    public UserAccess(IdentityDbContext db, int rows, IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
        rows_per_page = rows;
    }

    public async Task<IEnumerable<UserIdentityInfoDto?>> GetAllUsersInfo(int page, CancellationToken cancellation)
    {
        IEnumerable<User> result;
        var frompage = (page-1)*rows_per_page;
        var topage = page*rows_per_page;
        try{

            string sqlQuery = $@"SELECT @Infosql FROM users ORDER BY regularization_stage DESC LIMIT @frompage, @topage";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new { Infosql, frompage, topage},
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
    public async Task<IEnumerable<UserProfileDto?>> GetAllUsersProfile(int page, CancellationToken cancellation)
    {
        
        IEnumerable<User> result;
        var frompage = (page-1)*rows_per_page;
        var topage = page*rows_per_page;
        try{
            string sqlQuery = $@"SELECT @Profilesql FROM users ORDER BY regularization_stage DESC LIMIT @frompage, @topage";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                parameters: new {Profilesql, frompage, topage},
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
=======
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
    
>>>>>>> main
}
