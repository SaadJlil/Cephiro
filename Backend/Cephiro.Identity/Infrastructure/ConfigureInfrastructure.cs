using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Infrastructure.Data;
using Cephiro.Identity.Infrastructure.Executors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cephiro.Identity.Infrastructure;

public static class ConfigureInfrastructure
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDapperConnection(configuration);
        services.AddEfDbContext(configuration);
        services.AddWriteRepositories();
        
        return services;
    }

    public static IServiceCollection AddDapperConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var DapperSettings = new DapperConfig();
        configuration.Bind(DapperConfig.SectionName, DapperSettings);

        services.AddSingleton<DapperConfig>();

        return services;
    }

    public static IServiceCollection AddEfDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<IdentityDbContext>(options => 
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseNpgsql(configuration.GetConnectionString("IdentityConnection"),
            builder => builder.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName)
        ));

        return services;
    }

    public static IServiceCollection AddWriteRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IUserAuthExecutor, UserAuthExecutor>();
        services.AddSingleton<IUserProfileExecutor, UserProfileExecutor>();
        
        return services;
    }
}