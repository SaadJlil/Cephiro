using Cephiro.Identity.Application.Reporting.Contracts.Request;
using MassTransit;

namespace Cephiro.Identity.Application.Reporting.Commands.Handlers;

public sealed class IncrementUserCountHandler : IConsumer<IncrementUserCountRequest>
{
    private readonly IReportExecute _metrics;
    public IncrementUserCountHandler(IReportExecute metrics)
    {
        _metrics = metrics;
    }

    public async Task Consume(ConsumeContext<IncrementUserCountRequest> context)
    {
        bool increment = true;

        await _metrics.UpdateUserCount(increment, context.CancellationToken);
        return;
    }
}