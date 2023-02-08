using Cephiro.Identity.Application.Authentication.Commands;
using MassTransit;

namespace Cephiro.Identity.Application.Profile;

public static class ProfileMediator
{
    public static void AddProfileMediator(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddProfileCommands();
        cfg.AddProfileQueries();
    }
}