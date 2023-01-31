using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Infrastructure.Data;
using Dapper;
using ErrorOr;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Executors;

public sealed class MetricsReportsExecutor : IMetricsReportsExecutor
{
    private readonly IOptionsMonitor<DapperConfig> _settings;
    public MetricsReportsExecutor(IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
    }
    
    public async Task<ErrorOr<bool>> DecrementUnverifiedUserCount(CancellationToken token)
    {
        int result = 0;
        string sql = $@"UPDATE metrics SET unverified_user_count = unverified_user_count - 1";
        CommandDefinition cmd = new(commandText: sql, cancellationToken: token);

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

    public async Task<ErrorOr<bool>> UpdateActiveUserCount(bool increment, CancellationToken token)
    {
        int result = 0;
        string sql;

        if(increment) sql = $@"Update metrics SET active_user_count = active_user_count + 1";
        else sql = $@"UPDATE metrics SET active_user_count = active_user_count - 1";
        
        CommandDefinition cmd = new(sql, cancellationToken: token);

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

    public async Task<ErrorOr<bool>> UpdateUserCount(bool increment, CancellationToken token)
    {
        int result = 0;
        string sql;

        if(increment) sql = $@"Update metrics SET user_count = active_user_count + 1";
        else sql = $@"UPDATE metrics SET user_count = active_user_count - 1";
        
        CommandDefinition cmd = new(sql, cancellationToken: token);

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