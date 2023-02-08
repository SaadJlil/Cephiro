using Cephiro.Identity.Application.Profile.Contracts.Response;
using Cephiro.Identity.Application.Profile.Queries;
using Cephiro.Identity.Application.Shared.Contracts;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Data.Access;

public sealed class ProfileAccess : IProfileAccess
{
    private readonly DapperConfig _settings;
    public ProfileAccess(IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings.CurrentValue;
    }

    public async Task<UserProfileResponse> GetUserProfile(Guid id, CancellationToken token)
    {
        UserProfileResponse result = new() {};

        string sql = 
            $@"SELECT 
                id as [Id]
                image_uri as [ImageUri] 
                description as [Description]
                first_name as [FirstName]
                middle_name as [MiddleName]
                last_name as [LastName]
                email as [Email]
                phone_number as [PhoneNumber]
            FROM users WHERE id = @id LIMIT 1";

        var query = new CommandDefinition(commandText: sql, parameters: new { id }, cancellationToken: token);

        try
        {
            using(NpgsqlConnection db = new(_settings.IdentityConnection))
            {
                db.Open();
                result = await db.QuerySingleOrDefaultAsync<UserProfileResponse>(query);    
                db.Close();
            }
        } 

        catch(InvalidOperationException exception)
        {
            Error error = new() { Code = 400, Message = $"This operation is invalid - {exception.Message}"};
            result.IsError = true; result.Error = error;

            return result;
        }
        
        catch (NpgsqlException exception)
        {
            Error error = new() { Code = 502, Message = $"Could not establish connection to the database - {exception.Message}"};     
            result.IsError = true; result.Error = error;

            return result;
        }

        if(result.Email is null)
        {
            Error error = new() { Code = 400, Message = $"This operation is invalid"};     
            result.IsError = true; result.Error = error;

            return result;
        }

        return result;
    }

    public async Task<UsersListResponse> GetUsers(int take, int skip, CancellationToken token)
    {
        UsersListResponse result = new() {};

        var sql = $@"SELECT 
            email as [Email] 
            phone_number as [Phone] 
            first_name as [FirstName] 
            last_name as [LastName]
            FROM users LIMIT @take 
            OFFSET @skip * @take";

        var query = new CommandDefinition(commandText: sql, parameters: new { take, skip }, cancellationToken: token);

        try
        {
            using(NpgsqlConnection db = new(_settings.IdentityConnection))
            {
                db.Open();
                result.Users = await db.QueryAsync<UserInfoResponse>(query);    
                db.Close();
            }
        } 

        catch(InvalidOperationException exception)
        {
            Error error = new() { Code = 400, Message = $"This operation is invalid - {exception.Message}"};
            result.Error = error;

            return result;
        }
        
        catch (NpgsqlException exception)
        {
            Error error = new() { Code = 502, Message = $"Could not establish connection to the database - {exception.Message}"};     
            result.Error = error;

            return result;
        }

        if(!result.Users.Any())
        {
            Error error = new() { Code = 400, Message = $"This operation is invalid"};     
            result.Error = error;

            return result;
        }

        return result;
    }

}