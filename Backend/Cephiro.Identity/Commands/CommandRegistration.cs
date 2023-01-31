using System.Reflection;
using Cephiro.Identity.Contracts.Request.Authentication;
using Cephiro.Identity.Contracts.Request.Profile;
using Cephiro.Identity.Contracts.Request.Reporting;
using MassTransit;

namespace Cephiro.Identity.Commands;

public static class CommandRegistration
{
    public static void AddCommandRegistration(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddConsumers(Assembly.GetExecutingAssembly());

        // authentication request clients
        cfg.AddRequestClient<ChangePasswordRequest>();
        cfg.AddRequestClient<UserLoginRequest>();
        cfg.AddRequestClient<UserLogoutRequest>();
        cfg.AddRequestClient<UserRegistrationRequest>();

        // profile request clients
        cfg.AddRequestClient<UpdatePhoneNumberRequest>();
        cfg.AddRequestClient<UpdateProfileBioRequest>();
        cfg.AddRequestClient<UpdateProfileImageRequest>();

        // reporting request clients
        cfg.AddRequestClient<DecrementActiveUserRequest>();
        cfg.AddRequestClient<DecrementUnverifiedUsersRequest>();
        cfg.AddRequestClient<IncrementActiveUsersRequest>();
        cfg.AddRequestClient<IncrementUserCountRequest>();
    }
}