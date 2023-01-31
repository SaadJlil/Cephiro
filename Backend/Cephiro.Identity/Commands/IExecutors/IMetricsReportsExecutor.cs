using ErrorOr;

namespace Cephiro.Identity.Commands.IExecutors;

public interface IMetricsReportsExecutor
{
    public Task<ErrorOr<bool>> UpdateUserCount(bool increment, CancellationToken token);
    public Task<ErrorOr<bool>> UpdateActiveUserCount(bool increment, CancellationToken token);
    public Task<ErrorOr<bool>> DecrementUnverifiedUserCount(CancellationToken token);
}