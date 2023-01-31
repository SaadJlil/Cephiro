using Cephiro.Identity.Queries.IAccessor;
using Cephiro.Identity.Contracts.Request.Metrics;
using MassTransit;
using ErrorOr;

namespace Cephiro.Identity.Queries.Handlers.Metrics;


public sealed class GetUserCountHandler: IConsumer<MetricsRequest>
{
    private readonly IMetricsAccess _metricaccess;

    public GetUserCountHandler(IMetricsAccess metricaccess)
    {
        _metricaccess = metricaccess;
    }

    public async Task Consume(ConsumeContext<MetricsRequest> context)
    {
        Guid id = context.Message.GetIdFromRequestHeaders();

        if(id == Guid.Empty)
        {
            await context.RespondAsync(Error.Validation("A wrong Id has been received"));
            await context.ConsumeCompleted;
        }

        var result = await _metricaccess.GetUserCount(context.CancellationToken);

        if(result.IsError)
        {
            await context.RespondAsync(result.FirstError);
            await context.ConsumeCompleted;
        }

        await context.RespondAsync(result.Value);
    }
}


