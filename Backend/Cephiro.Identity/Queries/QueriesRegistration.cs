using MassTransit;
using System.Reflection;
using Cephiro.Identity.Contracts.Request.Info;
using Cephiro.Identity.Contracts.Request.Profile;


public static class QueriesRegistration 
{
    public static void AddQueriesRegistration(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddConsumers(Assembly.GetExecutingAssembly());

        //Info
        cfg.AddRequestClient<UserInfoRequest>();
        
        //Profile
        cfg.AddRequestClient<UserProfileRequest>();

        //Metrics
        cfg.AddRequestClient<UserProfileRequest>();
   }
}