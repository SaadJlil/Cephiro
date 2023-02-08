using Cephiro.Identity.Application.Profile.Contracts.Request;
using MassTransit;

namespace Cephiro.Identity.Application.Authentication.Commands;

public static class ProfileCommands
{
    public static void AddProfileCommands(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddRequestClient<UpdatePhoneNumberRequest>();
        cfg.AddRequestClient<UpdateProfileBioRequest>();
        cfg.AddRequestClient<UpdateProfileImageRequest>();
    }
}