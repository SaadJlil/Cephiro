using Cephiro.Identity.Application.Reporting.Contracts.Request;
using MassTransit;

namespace Cephiro.Identity.Application.Reporting.Commands.Handlers;

public sealed class DecrementActiveUsersHandler : IConsumer<DecrementActiveUserRequest>
{
    private readonly IReportExecute _metrics;
    public DecrementActiveUsersHandler(IReportExecute metrics)
    {
        _metrics = metrics;
    }
    public async Task Consume(ConsumeContext<DecrementActiveUserRequest> context)
    {
        bool increment = false;

        await _metrics.UpdateActiveUserCount(increment, context.CancellationToken);
        return;
    }
}