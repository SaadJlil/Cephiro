using Cephiro.Identity.Application.Reporting.Commands;
using Cephiro.Identity.Application.Shared.Contracts.Internal;
using Cephiro.Identity.Infrastructure.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Executors;

public sealed class ReportExecute : IReportExecute
{
    private readonly IOptionsMonitor<DapperConfig> _settings;
    public ReportExecute(IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
    }
    
    public async Task<DbWriteInternal> DecrementUnverifiedUserCount(CancellationToken token)
    {
        DbWriteInternal result = new() {ChangeCount = 0, Error = null};
        string sql = $@"UPDATE metrics SET unverified_user_count = unverified_user_count - 1";
        CommandDefinition cmd = new(commandText: sql, cancellationToken: token);

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

    public async Task<DbWriteInternal> UpdateActiveUserCount(bool increment, CancellationToken token)
    {
        DbWriteInternal result = new() {ChangeCount = 0, Error = null};
        string sql;

        if(increment) sql = $@"Update metrics SET active_user_count = active_user_count + 1";
        else sql = $@"UPDATE metrics SET active_user_count = active_user_count - 1";
        
        CommandDefinition cmd = new(sql, cancellationToken: token);

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

    public async Task<DbWriteInternal> UpdateUserCount(bool increment, CancellationToken token)
    {
        DbWriteInternal result = new() {ChangeCount = 0, Error = null};
        string sql;

        if(increment) sql = $@"Update metrics SET user_count = active_user_count + 1";
        else sql = $@"UPDATE metrics SET user_count = active_user_count - 1";
        
        CommandDefinition cmd = new(sql, cancellationToken: token);

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