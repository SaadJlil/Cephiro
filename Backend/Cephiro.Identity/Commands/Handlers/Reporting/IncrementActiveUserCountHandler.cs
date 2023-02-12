using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Contracts.Request.Reporting;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers.Reporting;

public sealed class IncrementActiveUserCountHandler : IConsumer<IncrementActiveUsersRequest>
{
    private readonly IMetricsReportsExecutor _metrics;
    public IncrementActiveUserCountHandler(IMetricsReportsExecutor metrics)
    {
        _metrics = metrics;
    }
    
    public async Task Consume(ConsumeContext<IncrementActiveUsersRequest> context)
    {
        bool increment = true;

        await _metrics.UpdateActiveUserCount(increment, context.CancellationToken);
        await context.ConsumeCompleted;
    }
}
