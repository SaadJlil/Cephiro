using Cephiro.Identity.Application.Profile.Commands;
using Cephiro.Identity.Application.Shared.Contracts.Internal;
using Cephiro.Identity.Contracts.Internal;
using Cephiro.Identity.Infrastructure.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Executors;

public sealed class ProfileExecute : IProfileExecute
{
    private readonly IOptionsMonitor<DapperConfig> _settings;
    public ProfileExecute(IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
    }

    public async Task<DbWriteInternal> UpdateDescription(Guid id, string description, CancellationToken token)
    {
        DbWriteInternal result = new() {ChangeCount = 0, Error = null};
        string sql = $@"
            UPDATE users 
            SET description = @description
            FROM users WHERE id = @id LIMIT 1";

        var param = new { description, id };

        var cmd = new CommandDefinition(commandText: sql, parameters: param, cancellationToken: token);

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection);
            db.Open();
            result.ChangeCount = await db.ExecuteAsync(cmd);
            db.Close();
        }

        catch (NpgsqlException exception)
        {
            result.Error = new() { Code = 504, Message = $"Could not establish a connection to the database - {exception.Message}"};
            return result;
        }

        return result;
    }

    public async Task<DbWriteInternal> UpdateUserImage(Guid id, Uri imageUri, CancellationToken token)
    {
        DbWriteInternal result = new() {ChangeCount = 0, Error = null};
        string sql = $@"
            UPDATE users 
            SET image_uri = @imageUri
            FROM users WHERE id = @id LIMIT 1";

        var param = new { imageUri = imageUri.LocalPath, id};

        CommandDefinition cmd = new(commandText: sql, parameters: param, cancellationToken: token);

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection);
            db.Open();
            result.ChangeCount = await db.ExecuteAsync(cmd);
            db.Close();
        }

        catch (NpgsqlException exception)
        {
            result.Error = new() { Code = 504, Message = $"Could not establish a connection to the database - {exception.Message}"};
            return result;
        }

        return result;
    }

    public async Task<DbWriteInternal> UpdateUserPhoneNumber(Guid id, string phonenumber, CancellationToken token)
    {
        DbWriteInternal result = new() {ChangeCount = 0, Error = null};
        string sql = $@"
            UPDATE users 
            SET phone_number = @phonenumber
            FROM users WHERE id = @id LIMIT 1";

        var param = new { phonenumber, id};

        CommandDefinition cmd = new(commandText: sql, parameters: param, cancellationToken: token);

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection);
            db.Open();
            result.ChangeCount = await db.ExecuteAsync(cmd);
            db.Close();
        }

        catch (NpgsqlException exception)
        {
            result.Error = new() { Code = 504, Message = $"Could not establish a connection to the database - {exception.Message}"};
            return result;
        }

        return result;
    }
}