using Cephiro.Identity.Application.Reporting.Contracts.Response;
using Cephiro.Identity.Application.Reporting.Queries;
using Cephiro.Identity.Application.Shared.Contracts;
using Cephiro.Identity.Domain.Entities;
using Dapper;
using MassTransit.Initializers;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cephiro.Identity.Infrastructure.Data.Access;

public sealed class ReportAccess : IReportAccess
{
    private readonly DapperConfig _settings;
    public ReportAccess(IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings.CurrentValue;
    }

    public async Task<MetricsResponse> GetUserMetrics(CancellationToken token)
    {
        MetricsResponse result = new() {};

        var sql = "SELECT * FROM metrics LIMIT 1";

        var query = new CommandDefinition(commandText: sql, cancellationToken: token);

        try
        {
            using(NpgsqlConnection db = new(_settings.IdentityConnection))
            {
                db.Open();
                var metrics = await db.QuerySingleOrDefaultAsync<Metrics>(query);

                result.ActiveUserCount = metrics.ActiveUserCount;
                result.LockoutEnabled = metrics.LockoutEnabled;
                result.UnverifiedUserCount = metrics.UnverifiedUserCount;
                result.UserCount = metrics.UserCount;
                result.Error = null;

                db.Close();
            }
        } 

        catch(InvalidOperationException exception)
        {
            Error error = new() { Code = 400, Message = $"This operation is invalid - {exception.Message}"};
            result.Error = error;

            return result;
        }
        
        catch (NpgsqlException exception)
        {
            Error error = new() { Code = 502, Message = $"Could not establish connection to the database - {exception.Message}"};     
            result.Error = error;

            return result;
        }

        return result;
    }
}