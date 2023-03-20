using Cephiro.Identity.Application.Reporting.Contracts.Request;
using MassTransit;

namespace Cephiro.Identity.Application.Reporting.Commands.Handlers;

public sealed class IncrementActiveUsersHandler : IConsumer<IncrementActiveUsersRequest>
{
    private readonly IReportExecute _metrics;
    public IncrementActiveUsersHandler(IReportExecute metrics)
    {
        _metrics = metrics;
    }
    
    public async Task Consume(ConsumeContext<IncrementActiveUsersRequest> context)
    {
        bool increment = true;

        await _metrics.UpdateActiveUserCount(increment, context.CancellationToken);
        return;
    }
}