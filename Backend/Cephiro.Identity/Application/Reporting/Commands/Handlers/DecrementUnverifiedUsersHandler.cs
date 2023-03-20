using Cephiro.Identity.Application.Reporting.Contracts.Request;
using MassTransit;

namespace Cephiro.Identity.Application.Reporting.Commands.Handlers;

public sealed class DecrementUnverifiedUsers : IConsumer<DecrementUnverifiedUsersRequest>
{
    private readonly IReportExecute _metrics;
    public DecrementUnverifiedUsers(IReportExecute metrics)
    {
        _metrics = metrics;
    }
    
    public async Task Consume(ConsumeContext<DecrementUnverifiedUsersRequest> context)
    {
        await _metrics.DecrementUnverifiedUserCount(context.CancellationToken);
        return;
    }
}