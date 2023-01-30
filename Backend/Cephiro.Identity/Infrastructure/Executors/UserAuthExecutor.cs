using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Commands.Utils;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.Enum;
using Cephiro.Identity.Domain.ValueObjects;
using Cephiro.Identity.Infrastructure.Data;
using Dapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Executors;

public sealed class UserAuthExecutor : IUserAuthExecutor
{
    private readonly IOptionsMonitor<DapperConfig> _settings;
    private readonly IdentityDbContext _db;
    public UserAuthExecutor(IdentityDbContext db, IOptionsMonitor<DapperConfig> settings)
    {
        _db = db;
        _settings = settings;
    }

    public async Task<ErrorOr<bool>> RegisterNewUser(User user, CancellationToken token)
    {
        int result;

        try
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            await _db.AddAsync(user, token);
            result = await _db.SaveChangesAsync(token);
            
        } 

        catch (DbUpdateException exception)
        {
            return Error.Failure(exception.Message);
        }

        finally
        {
            _db.ChangeTracker.Clear();
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        return result > 0;
    }

    public async Task<ErrorOr<bool>> UpdateUserStatus(Guid id, Activity status, CancellationToken token)
    {

        string sqlCmd = $@"UPDATE users SET status = @status WHERE id = @id LIMIT 1";
        var cmdParams = new { status, id };

        var cmd = new CommandDefinition(commandText: sqlCmd, parameters: cmdParams, cancellationToken: token);
        int result = 0;

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection);
            db.Open();
            await db.ExecuteAsync(cmd);
            db.Close();
        }

        catch(NpgsqlException exception)
        {
            return Error.Failure(exception.Message);
        }

        return result > 0;
    }


    public async Task<ErrorOr<bool>> ChangeUserPassword(Guid id, byte[] passwordHash, byte[] passwordSalt, CancellationToken token)
    {
        string sql = $@"
            UPDATE users 
            SET password_hash = @newHash
            SET password_salt = @newSalt 
            FROM users WHERE id = @id LIMIT 1";

        var param = new { newHash = passwordHash, newSalt = passwordSalt, id};
        var cmd = new CommandDefinition(commandText: sql, parameters: param, cancellationToken: token);
        int result = 0;

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection);
            db.Open();
            result = await db.ExecuteAsync(cmd);
            db.Close();
        }

        catch(NpgsqlException exception)
        {
            return Error.Failure(exception.Message);
        }

        return result > 0;
    }

}