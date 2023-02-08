using Cephiro.Identity.Application.Authentication.Commands;
using MassTransit;

namespace Cephiro.Identity.Application.Authentication;
public static class AuthMediator
{
    public static void AddAuthenticationMediator(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddAuthenticationCommands();
        cfg.AddAuthenticationQueries();
    }
}