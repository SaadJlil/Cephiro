using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Commands.Utils;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.Enum;
using Cephiro.Identity.Domain.ValueObjects;
using Cephiro.Identity.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Executors;

public sealed class UserAuthExecutor : IUserAuthExecutor
{
    private readonly NpgsqlConnection _connect;
    private readonly IdentityDbContext _db;
    public UserAuthExecutor(IdentityDbContext db, IOptionsMonitor<DapperConfig> settings)
    {
        _db = db;
        _connect = new NpgsqlConnection(settings.CurrentValue.IdentityConnection);
        _connect.Open();
    }

    public async Task<bool> RegisterNewUser(User user, CancellationToken cancellation)
    {
        int result;

        try
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            await _db.AddAsync(user, cancellation);
            result = await _db.SaveChangesAsync(cancellation);
            
        } 

        finally
        {
            _db.ChangeTracker.Clear();
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        return result > 0;
    }

    public async Task<User?> SignUserIn(string email, Password password, CancellationToken cancellation)
    {
        string sqlQuery = $@"SELECT * FROM users WHERE email_address = @email LIMIT 1";
        var query = new CommandDefinition(
            commandText: sqlQuery, 
            parameters: new { email }, 
            cancellationToken: cancellation);

        var result = await _connect.QuerySingleOrDefaultAsync<User>(query);
        
        var passwordHash = result.PasswordHash!;
        var passwordSalt = result.PasswordSalt!;

        if(!PasswordHashProvider.VerifyPassword(password.Value, passwordHash, passwordSalt))
            return null;

        string sqlCmd = $@"UPDATE users SET status = @status WHERE email_address = @email LIMIT 1";
        var cmdParams = new { status = (int)Activity.ACTIVE, email };
        var cmd = new CommandDefinition(
            commandText: sqlCmd,
            parameters: cmdParams,
            cancellationToken: cancellation
        );
        await _connect.ExecuteAsync(cmd);

        return result;
    }

    public async Task<bool> SignUserOut(Guid id, CancellationToken cancellation)
    {
        string sql = $@"UPDATE users SET status = @status WHERE id = @id LIMIT 1";
        var param = new { status = (int)Activity.DISCONNECTED, id };

        var cmd = new CommandDefinition(commandText: sql, parameters: param, cancellationToken: cancellation);
        
        await _connect.ExecuteAsync(cmd);

        return true;
    }
}