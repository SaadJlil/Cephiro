using Cephiro.Identity.Domain.Exceptions;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Queries.IAccessor;
using Cephiro.Identity.Infrastructure.Data;
using Cephiro.Identity.Contracts.Response;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Options;
using ErrorOr;




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

    public async Task<ErrorOr<List<UserInfoResponse>>> GetAllUsersInfo(int page, CancellationToken cancellation)
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

            return result.Select(x => new UserInfoResponse{
                LastName = x.LastName,
                FirstName =  x.FirstName,
                Email = x.Email
            }).ToList();

            
        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);
        }
    }
    public async Task<ErrorOr<UserInfoResponse>> GetUserInfoById(Guid Id, CancellationToken cancellation)
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

            
            return new UserInfoResponse{
                LastName = result.LastName,
                FirstName =  result.FirstName,
                Email = result.Email
            };
        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);
        }


    }

    public async Task<ErrorOr<UserInfoResponse>> GetUserInfoByEmail(string Email, CancellationToken cancellation)
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

            return new UserInfoResponse{
                LastName = result.LastName,
                FirstName =  result.FirstName,
                Email = result.Email
            };
        }
        catch(NpgsqlException exception){

            return Error.Failure(exception.Message);
        }


    }
    public async Task<ErrorOr<UserInfoResponse>> GetUserInfoByPhone(string PhoneNumber, CancellationToken cancellation)
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

            return new UserInfoResponse{
                LastName = result.LastName,
                FirstName =  result.FirstName,
                Email = result.Email
            };
        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);

        }


    }




    //Profile
    public async Task<ErrorOr<List<UserProfileResponse>>> GetAllUsersProfile(int page, CancellationToken cancellation)
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

            return result.Select(x => new UserProfileResponse{
                ImageUri = x.ImageUri,
                FirstName = x.FirstName, 
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber 
            }).ToList();

       }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);
        }
    }
    public async Task<ErrorOr<UserProfileResponse>> GetUserProfileById(Guid Id, CancellationToken cancellation)
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


            return new UserProfileResponse{
                ImageUri = result.ImageUri,
                FirstName = result.FirstName, 
                MiddleName = result.MiddleName,
                LastName = result.LastName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber 
            };
        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);
        }
   }

    public async Task<ErrorOr<UserProfileResponse>> GetUserProfileByEmail(string Email, CancellationToken cancellation)
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

            return new UserProfileResponse{
                ImageUri = result.ImageUri,
                FirstName = result.FirstName, 
                MiddleName = result.MiddleName,
                LastName = result.LastName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber 
            };

        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);

        }


    }
    public async Task<ErrorOr<UserProfileResponse>> GetUserProfileByPhone(string PhoneNumber, CancellationToken cancellation)
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

            return new UserProfileResponse{
                ImageUri = result.ImageUri,
                FirstName = result.FirstName, 
                MiddleName = result.MiddleName,
                LastName = result.LastName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber 
            };

        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);
        }
   } 
}