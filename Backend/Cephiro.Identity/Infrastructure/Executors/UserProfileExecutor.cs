using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Commands.Utils;
using Cephiro.Identity.Domain.ValueObjects;
using Cephiro.Identity.Infrastructure.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Executors;

public sealed class UserProfileExecutor : IUserProfileExecutor
{
    private readonly NpgsqlConnection _connect;
    private readonly IdentityDbContext _db;
    public UserProfileExecutor(IdentityDbContext db, IOptionsMonitor<DapperConfig> settings)
    {
        _db = db;
        _connect = new NpgsqlConnection(settings.CurrentValue.IdentityConnection);
        _connect.Open();
    }
    
    public async Task<bool> ChangeUserPassword(Guid id, Password oldPass, Password newPass)
    {
        string query = $@"SELECT password_hash, password_salt FROM users WHERE id = @id LIMIT 1";
        var queryParams = new { id };
        var result = await _connect.QuerySingleOrDefaultAsync<(byte[] passwordHash, byte[] passwordSalt)>(query, queryParams);
        
        var passwordHash = result.passwordHash;
        var passwordSalt = result.passwordSalt;

        if(!PasswordHashProvider.VerifyPassword(oldPass.Value, passwordHash, passwordSalt))
            return false;

        PasswordHashProvider.GeneratePasswordHash(newPass.Value, out byte[] newHash, out byte[] newSalt);

        if(newHash is null || newSalt is null)
            return false;
        
        string command = $@"
            UPDATE users 
            SET password_hash = @newHash
            SET password_salt = @newSalt 
            FROM users WHERE id = @id LIMIT 1";

        var cmdParams = new { newHash, newSalt, id};

        return await _connect.ExecuteAsync(command, cmdParams) > 0;
    }

    public async Task<bool> UpdateDescription(Guid id, string description)
    {
        string command = $@"
            UPDATE users 
            SET description = @description
            FROM users WHERE id = @id LIMIT 1";

        var cmdParams = new { description, id};

        return await _connect.ExecuteAsync(command, cmdParams) > 0;
    }

    public async Task<bool> UpdateUserImage(Guid id, Uri imageUri)
    {
        string command = $@"
            UPDATE users 
            SET image_uri = @imageUri
            FROM users WHERE id = @id LIMIT 1";

        var cmdParams = new { imageUri = imageUri.LocalPath, id};

        return await _connect.ExecuteAsync(command, cmdParams) > 0;
    }

    public async Task<bool> UpdateUserPhoneNumber(Guid id, string phonenumber)
    {
        string command = $@"
            UPDATE users 
            SET phone_number = @phonenumber
            FROM users WHERE id = @id LIMIT 1";

        var cmdParams = new { phonenumber, id};

        return await _connect.ExecuteAsync(command, cmdParams) > 0;
    }
}