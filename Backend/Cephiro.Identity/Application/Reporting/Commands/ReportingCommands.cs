using Cephiro.Identity.Application.Reporting.Contracts.Request;
using MassTransit;

namespace Cephiro.Identity.Application.Authentication.Commands;

public static class ReportingCommands
{
    public static void AddReportingCommands(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddRequestClient<DecrementActiveUserRequest>();
        cfg.AddRequestClient<DecrementUnverifiedUsersRequest>();
        cfg.AddRequestClient<IncrementActiveUsersRequest>();
        cfg.AddRequestClient<IncrementUserCountRequest>();
    }
}