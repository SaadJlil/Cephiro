using Cephiro.Identity.Application.Authentication.Commands;
using Cephiro.Identity.Application.Shared.Contracts.Internal;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.Enum;
using Cephiro.Identity.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Executors;

public sealed class AuthExecute : IAuthExecute
{
    private readonly IOptionsMonitor<DapperConfig> _settings;
    private readonly IdentityDbContext _db;
    public AuthExecute(IdentityDbContext db, IOptionsMonitor<DapperConfig> settings)
    {
        _db = db;
        _settings = settings;
    }

    public async Task<DbWriteInternal> RegisterNewUser(User user, CancellationToken token)
    {
        DbWriteInternal result = new() { ChangeCount = 0, Error = null};

        try
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            await _db.AddAsync(user, token);
            result.ChangeCount = await _db.SaveChangesAsync(token);
            
        } 

        catch (DbUpdateException exception)
        {
            return new DbWriteInternal() 
            { 
                ChangeCount = 0, Error = new() 
                {
                    Code = 502, 
                    Message = $"Could not establish connection to the database - ${exception.Message + exception.InnerException}",
                }};
        }

        finally
        {
            _db.ChangeTracker.Clear();
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        return result;
    }

    public async Task<DbWriteInternal> UpdateUserStatus(Guid id, Activity status, CancellationToken token)
    {
        DbWriteInternal result = new() { ChangeCount = 0, Error = null};
        string sqlCmd = $@"UPDATE users SET status = @status WHERE id = @id LIMIT 1";
        var cmdParams = new { status, id };

        var cmd = new CommandDefinition(commandText: sqlCmd, parameters: cmdParams, cancellationToken: token);

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection);
            db.Open();
            result.ChangeCount = await db.ExecuteAsync(cmd);
            db.Close();
        }

        catch(NpgsqlException exception)
        {
            result.Error = new() {Code = 504, Message = $"Could not establish a connection to the database - {exception.Message}"};
            return result;
        }

        return result;
    }


    public async Task<DbWriteInternal> ChangeUserPassword(Guid id, byte[] passwordHash, byte[] passwordSalt, CancellationToken token)
    {
        DbWriteInternal result = new() {ChangeCount = 0, Error = null};
        string sql = $@"
            UPDATE users 
            SET password_hash = @newHash
            SET password_salt = @newSalt 
            FROM users WHERE id = @id LIMIT 1";

        var param = new { newHash = passwordHash, newSalt = passwordSalt, id};
        var cmd = new CommandDefinition(commandText: sql, parameters: param, cancellationToken: token);

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.IdentityConnection);
            db.Open();
            result.ChangeCount = await db.ExecuteAsync(cmd);
            db.Close();
        }

        catch(NpgsqlException exception)
        {
            result.Error = new() { Code = 504, Message = $"Could not establish a connection with the database {exception.Message}"};
            return result;
        }

        return result;
    }

}