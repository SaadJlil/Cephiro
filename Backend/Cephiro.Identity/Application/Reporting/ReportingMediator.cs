using Cephiro.Identity.Application.Authentication.Commands;
using MassTransit;

namespace Cephiro.Identity.Application.Reporting;

public static class ReportingMediator
{
    public static void AddReportingMediator(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddReportingCommands();
        cfg.AddReportingQueries();
    }
}