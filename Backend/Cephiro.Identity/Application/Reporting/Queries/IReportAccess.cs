using Cephiro.Identity.Application.Reporting.Contracts.Response;

namespace Cephiro.Identity.Application.Reporting.Queries;

public interface IReportAccess
{
    public Task<MetricsResponse> GetUserMetrics(CancellationToken token);
}