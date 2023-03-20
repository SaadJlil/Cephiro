using Cephiro.Identity.Application.Authentication.Contracts.Internal;
using Cephiro.Identity.Application.Authentication.Queries;
using Cephiro.Identity.Application.Shared.Contracts;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Infrastructure.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Accessors;

public sealed class AuthAccess : IAuthAccess
{
    private readonly DapperConfig _settings;
    public AuthAccess(IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings.CurrentValue;
    }

    public async Task<UserEntityInternal> GetUserByEmail(string email, CancellationToken token)
    {
        Error error = new() { Code = 400, Message = "This email address is incorrect"};
        UserEntityInternal result = new() { User = null, Error = error};

        string sqlQuery = $@"SELECT * FROM users WHERE email_address = @email LIMIT 1";
        var query = new CommandDefinition(commandText: sqlQuery, parameters: new { email }, cancellationToken: token);

        try
        {
            using(NpgsqlConnection db = new(_settings.IdentityConnection))
            {
                db.Open();
                result.User = await db.QuerySingleOrDefaultAsync<User>(query);    
                db.Close();
            }
        } 
        
        catch (NpgsqlException exception)
        {
            error.Code = 504; error.Message = $"Could not establish connection to the database - ${exception.Message}";
            return result;
        }

        if(result.User is null)
                return result;

        return result;
    }

    public async Task<UserEntityInternal> GetUserByPhoneNumber(string phoneNumber, CancellationToken token)
    {
        Error error = new() { Code = 400, Message = "This email address is incorrect"};
        UserEntityInternal result = new() { User = null, Error = error};

        string sqlQuery = $@"SELECT * FROM users WHERE phone_number = @phoneNumber LIMIT 1";
        var query = new CommandDefinition(commandText: sqlQuery, parameters: new { phoneNumber }, cancellationToken: token);

        try
        {
            using(NpgsqlConnection db = new(_settings.IdentityConnection))
            {
                db.Open();
                result.User = await db.QuerySingleOrDefaultAsync<User>(query);    
                db.Close();
            }
        } 
        
        catch (NpgsqlException exception)
        {
            error.Code = 504; error.Message = $"Could not establish connection to the database - ${exception.Message}";
            return result;
        }

        if(result.User is null)
                return result;

        return result;
    }
    
    public async Task<HashedPasswordInternal> GetHashedPassword(Guid id, CancellationToken token)
    {
        string sql = $@"SELECT password_hash, password_salt FROM users WHERE id = @id LIMIT 1";
        var param = new { id };
        var cmd = new CommandDefinition(commandText: sql, parameters: param, cancellationToken: token);

        Error error = new() { Code = 400, Message = "Please, refresh your connection and try again"};
        HashedPasswordInternal result = new() {PasswordHash = null, PasswordSalt = null, Error = error};

        try
        {
            using(NpgsqlConnection db = new(_settings.IdentityConnection))
            {
                db.Open();
                var (hash, salt) = await db.QuerySingleOrDefaultAsync<(byte[] hash, byte[] salt)>(cmd);
                result.PasswordHash = hash; result.PasswordSalt = salt; result.Error = null;
                db.Close();
            }

            if(result.PasswordHash is null || result.PasswordSalt is null) 
                return result;
        }

        catch(NpgsqlException exception)
        {
            error.Code = 502; error.Message = $"Could not establish a connection to the database {exception.Message}";
            return result;
        }

        return result;
    }
}