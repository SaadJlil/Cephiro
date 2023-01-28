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

    public async Task<bool> RegisterNewUser(User user)
    {
        int result;

        try
        {
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            await _db.AddAsync(user);
            result = await _db.SaveChangesAsync();
            
        } 

        finally
        {
            _db.ChangeTracker.Clear();
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        return result > 0;
    }

    public async Task<bool> SignUserIn(string email, Password password)
    {
        string query = $@"SELECT password_hash, password_salt FROM users WHERE email_address = @email LIMIT 1";
        var queryParams = new { email };
        var result = await _connect.QuerySingleOrDefaultAsync<(byte[] passwordHash, byte[] passwordSalt)>(query, queryParams);
        
        var passwordHash = result.passwordHash;
        var passwordSalt = result.passwordSalt;

        if(!PasswordHashProvider.VerifyPassword(password.Value, passwordHash, passwordSalt))
            return false;

        string command = $@"UPDATE users SET status = @status WHERE email_address = @email LIMIT 1";
        var cmdParams = new { status = (int)Activity.ACTIVE, email };
        await _connect.ExecuteAsync(command, cmdParams);

        return true;
    }

    public async Task<bool> SignUserOut(Guid id)
    {
        string command = $@"UPDATE users SET status = @status WHERE id = @id LIMIT 1";
        var cmdParams = new { status = (int)Activity.DISCONNECTED, id };
        await _connect.ExecuteAsync(command, cmdParams);

        return true;
    }
}