using System.Reflection;
using Cephiro.Identity.Application.Authentication;
using Cephiro.Identity.Application.Authentication.Commands;
using Cephiro.Identity.Application.Profile;
using Cephiro.Identity.Application.Reporting;
using MassTransit;

namespace Cephiro.Identity.Application;

public static class Mediator
{
    public static void AddIdentityMediator(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddConsumers(Assembly.GetExecutingAssembly());
        
        cfg.AddAuthenticationMediator();
        cfg.AddProfileMediator();
        cfg.AddReportingMediator();
    }
}