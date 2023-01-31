using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Contracts.Request.Reporting;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers.Reporting;

public sealed class DecrementUnverifiedUserCountHandler : IConsumer<DecrementUnverifiedUsersRequest>
{
    private readonly IMetricsReportsExecutor _metrics;
    public DecrementUnverifiedUserCountHandler(IMetricsReportsExecutor metrics)
    {
        _metrics = metrics;
    }
    
    public async Task Consume(ConsumeContext<DecrementUnverifiedUsersRequest> context)
    {
        await _metrics.DecrementUnverifiedUserCount(context.CancellationToken);
        await context.ConsumeCompleted;
    }
}