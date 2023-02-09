//using Cephiro.Identity.Commands.IExecutors;
//using Cephiro.Identity.Infrastructure.Executors;
using Npgsql;
using Cephiro.Listings.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cephiro.Listings.Infrastructure;

public static class ConfigureInfrastructure
{

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
//        services.AddDapperConnection(configuration);
        System.Console.WriteLine("happened");
        services.AddEfDbContext(configuration);
        //services.AddWriteRepositories();
        
        return services;
    }
/*
    public static IServiceCollection AddDapperConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var DapperSettings = new DapperConfig();
        configuration.Bind(DapperConfig.SectionName, DapperSettings);

        services.AddSingleton<DapperConfig>();

        return services;
    }
*/

    public static IServiceCollection AddEfDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<ListingsDbContext>(options => 
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseNpgsql(configuration.GetConnectionString("ListingsConnection"),
            builder => builder.MigrationsAssembly(typeof(ListingsDbContext).Assembly.FullName)
        ));

        return services;
    }
/*
    public static IServiceCollection AddWriteRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IUserAuthExecutor, UserAuthExecutor>();
        services.AddSingleton<IUserProfileExecutor, UserProfileExecutor>();
        
        return services;
    }
*/
}