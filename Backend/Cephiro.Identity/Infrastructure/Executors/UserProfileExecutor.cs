using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Infrastructure.Data;
using Dapper;
using ErrorOr;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Executors;

public sealed class UserProfileExecutor : IUserProfileExecutor
{
    private readonly IOptionsMonitor<DapperConfig> _settings;
    public UserProfileExecutor(IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
    }

    public async Task<ErrorOr<bool>> UpdateDescription(Guid id, string description, CancellationToken token)
    {
        string sql = $@"
            UPDATE users 
            SET description = @description
            FROM users WHERE id = @id LIMIT 1";

        var param = new { description, id };

        var cmd = new CommandDefinition(commandText: sql, parameters: param, cancellationToken: token);
        int result = 0;

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection);
            db.Open();
            result = await db.ExecuteAsync(cmd);
            db.Close();
        }

        catch (NpgsqlException exception)
        {
            return Error.Failure(exception.Message);
        }

        return result > 0;
    }

    public async Task<ErrorOr<bool>> UpdateUserImage(Guid id, Uri imageUri, CancellationToken token)
    {
        string sql = $@"
            UPDATE users 
            SET image_uri = @imageUri
            FROM users WHERE id = @id LIMIT 1";

        var param = new { imageUri = imageUri.LocalPath, id};

        CommandDefinition cmd = new(commandText: sql, parameters: param, cancellationToken: token);
        int result = 0;

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection);
            db.Open();
            result = await db.ExecuteAsync(cmd);
            db.Close();
        }

        catch (NpgsqlException exception)
        {
            return Error.Failure(exception.Message);
        }

        return result > 0;
    }

    public async Task<ErrorOr<bool>> UpdateUserPhoneNumber(Guid id, string phonenumber, CancellationToken token)
    {
        string sql = $@"
            UPDATE users 
            SET phone_number = @phonenumber
            FROM users WHERE id = @id LIMIT 1";

        var param = new { phonenumber, id};

        CommandDefinition cmd = new(commandText: sql, parameters: param, cancellationToken: token);
        int result = 0;

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection);
            db.Open();
            result = await db.ExecuteAsync(cmd);
            db.Close();
        }

        catch (NpgsqlException exception)
        {
            return Error.Failure(exception.Message);
        }

        return result > 0;
    }
}