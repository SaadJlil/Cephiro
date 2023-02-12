using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Contracts.Request.Reporting;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers.Reporting;

public sealed class DecrementActiveUserCountHandler : IConsumer<DecrementActiveUserRequest>
{
    private readonly IMetricsReportsExecutor _metrics;
    public DecrementActiveUserCountHandler(IMetricsReportsExecutor metrics)
    {
        _metrics = metrics;
    }
    public async Task Consume(ConsumeContext<DecrementActiveUserRequest> context)
    {
        bool increment = false;

        await _metrics.UpdateActiveUserCount(increment, context.CancellationToken);
        await context.ConsumeCompleted;
    }
}