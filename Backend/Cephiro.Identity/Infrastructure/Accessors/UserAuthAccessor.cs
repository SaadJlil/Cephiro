using Cephiro.Identity.Contracts.Response;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.ValueObjects;
using Cephiro.Identity.Infrastructure.Data;
using Cephiro.Identity.Queries.IAccessors;
using Dapper;
using ErrorOr;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Accessors;

public sealed class UserAuthAccessor : IUserAuthAccessor
{
    private readonly IOptionsMonitor<DapperConfig> _settings;
    public UserAuthAccessor(IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
    }

    public async Task<ErrorOr<User>> GetUserByEmail(string email, CancellationToken token)
    {
        User result;

        string sqlQuery = $@"SELECT * FROM users WHERE email_address = @email LIMIT 1";
        var query = new CommandDefinition(commandText: sqlQuery, parameters: new { email }, cancellationToken: token);

        try
        {
            using(NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QuerySingleOrDefaultAsync<User>(query);    
                db.Close();
            }

            if(result is null)
                return Error.NotFound("The email you entered is incorrect");
        } 
        
        catch (NpgsqlException exception)
        {
            return Error.Failure(exception.Message);
        }

        return result;
    }
    
    public async Task<ErrorOr<HashedPasswordResponse>> GetHashedPassword(Guid id, Password password, CancellationToken token)
    {
        string sql = $@"SELECT password_hash, password_salt FROM users WHERE id = @id LIMIT 1";
        var param = new { id };
        var cmd = new CommandDefinition(commandText: sql, parameters: param, cancellationToken: token);

        HashedPasswordResponse result; 

        try
        {
            using(NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QuerySingleOrDefaultAsync<HashedPasswordResponse>(cmd);
                db.Close();
            }

            if(result.PasswordHash is null || result.PasswordSalt is null) 
                return Error.Validation("Please, refresh your connection and try again");
        }

        catch(NpgsqlException exception)
        {
            return Error.Failure(exception.Message);
        }

        return result;
    }
}