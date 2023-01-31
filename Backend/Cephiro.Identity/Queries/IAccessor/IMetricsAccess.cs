using Cephiro.Identity.Contracts.Response.Metrics;
using Cephiro.Identity.Domain.Entities;
using ErrorOr;




namespace Cephiro.Identity.Queries.IAccessor;

public interface IMetricsAccess
{
    public Task<ErrorOr<UnverifiedUserCountResponse>> GetUnverifiedUserCount(CancellationToken cancellation);
    public Task<ErrorOr<ActiveUserCountResponse>> GetActiveUserCount(CancellationToken cancellation);
    public Task<ErrorOr<UserCountResponse>> GetUserCount(CancellationToken cancellation);
    public Task<ErrorOr<UserLockoutResponse>> GetUserLockout(CancellationToken cancellation);
    public Task<ErrorOr<Metrics>> GetUserMetrics(CancellationToken cancellation);

}