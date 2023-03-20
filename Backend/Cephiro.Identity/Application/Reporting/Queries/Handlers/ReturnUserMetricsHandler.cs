using Cephiro.Identity.Application.Reporting.Contracts.Request;
using Cephiro.Identity.Application.Reporting.Contracts.Response;
using MassTransit;

namespace Cephiro.Identity.Application.Reporting.Queries.Handlers;

public sealed class ReturnUserMetricsHandler : IConsumer<GetUserMetricsRequest>
{
    private readonly IReportAccess _access;
    public ReturnUserMetricsHandler(IReportAccess access)
    {
        _access = access;
    }
    public async Task Consume(ConsumeContext<GetUserMetricsRequest> context)
    {
        var result = await _access.GetUserMetrics(context.CancellationToken);

        await context.RespondAsync<MetricsResponse>(result);
        return;
    }
}