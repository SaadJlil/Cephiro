using Cephiro.Identity.Application.Reporting.Contracts.Request;
using MassTransit;

namespace Cephiro.Identity.Application.Authentication.Commands;

public static class ReportingQueries
{
    public static void AddReportingQueries(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddRequestClient<GetUserMetricsRequest>();
    }
}