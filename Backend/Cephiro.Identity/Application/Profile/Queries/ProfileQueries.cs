using Cephiro.Identity.Application.Profile.Contracts.Request;
using MassTransit;

namespace Cephiro.Identity.Application.Authentication.Commands;

public static class ProfileQueries
{
    public static void AddProfileQueries(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddRequestClient<UserListRequest>();
        cfg.AddRequestClient<UserProfileRequest>();
    }
}