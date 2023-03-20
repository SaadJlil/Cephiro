using Cephiro.Identity.Application.Authentication.Contracts.Request;
using MassTransit;

namespace Cephiro.Identity.Application.Authentication.Commands;

public static class AuthCommands
{
    public static void AddAuthenticationCommands(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddRequestClient<ChangePasswordRequest>();
        cfg.AddRequestClient<UserLoginRequest>();
        cfg.AddRequestClient<UserLogoutRequest>();
        cfg.AddRequestClient<UserRegistrationRequest>();
    }
}