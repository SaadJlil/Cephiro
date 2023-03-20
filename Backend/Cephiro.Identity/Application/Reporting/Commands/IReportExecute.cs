using Cephiro.Identity.Application.Shared.Contracts.Internal;

namespace Cephiro.Identity.Application.Reporting.Commands;

public interface IReportExecute
{
    public Task<DbWriteInternal> UpdateUserCount(bool increment, CancellationToken token);
    public Task<DbWriteInternal> UpdateActiveUserCount(bool increment, CancellationToken token);
    public Task<DbWriteInternal> DecrementUnverifiedUserCount(CancellationToken token);
}