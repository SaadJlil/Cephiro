using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Queries.IAccessor;
using Cephiro.Identity.Infrastructure.Data;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Options;
using ErrorOr;
using Cephiro.Identity.Contracts.Response.Metrics;


namespace Cephiro.Identity.Infrastructure.Accessor;

public sealed class MetricsAccess: IMetricsAccess
{
    private IOptionsMonitor<DapperConfig> _settings;

    public MetricsAccess(IdentityDbContext db, IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
    }

    public async Task<ErrorOr<ActiveUserCountResponse>> GetActiveUserCount(CancellationToken cancellation)
    {
        int result;
        try{
            string sqlQuery = $@"SELECT active_user_count FROM users";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<int>(query);
                db.Close();
            }

            return new ActiveUserCountResponse{
                ActiveUserCount = result
            };
            
        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);
        }
    }

    public async Task<ErrorOr<UnverifiedUserCountResponse>> GetUnverifiedUserCount(CancellationToken cancellation)
    {
        int result;
        try{
            string sqlQuery = $@"SELECT active_user_count FROM metrics";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<int>(query);
                db.Close();
            }

            return new UnverifiedUserCountResponse{
                UnverifiedUserCount = result
            };
            
        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);
        }
    }
    
    public async Task<ErrorOr<UserCountResponse>> GetUserCount(CancellationToken cancellation)
    {
        int result;
        try{
            string sqlQuery = $@"SELECT user_count FROM metrics";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<int>(query);
                db.Close();
            }

            return new UserCountResponse{
                UserCount = result
            };
            
        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);
        }
    }

    public async Task<ErrorOr<UserLockoutResponse>> GetUserLockout(CancellationToken cancellation)
    {
        bool result;
        try{
            string sqlQuery = $@"SELECT lockout_enabled FROM metrics";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<bool>(query);
                db.Close();
            }

            return new UserLockoutResponse{
                LockoutEnabled = result 
            };
            
        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);
        }
    }

    public async Task<ErrorOr<Metrics>> GetUserMetrics(CancellationToken cancellation)
    {
        Metrics result;
        try{
            string sqlQuery = $@"SELECT * FROM metrics";

            var query = new CommandDefinition(
                commandText: sqlQuery, 
                cancellationToken: cancellation);

            using(NpgsqlConnection db = new NpgsqlConnection(_settings.CurrentValue.IdentityConnection))
            {
                db.Open();
                result = await db.QueryFirstOrDefaultAsync<Metrics>(query);
                db.Close();
            }

           return result; 

        }
        catch(NpgsqlException exception){
            return Error.Failure(exception.Message);
        }
    }
}