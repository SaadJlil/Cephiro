//using Cephiro.Identity.Commands.IExecutors;
//using Cephiro.Identity.Infrastructure.Executors;
using Npgsql;
using Cephiro.Listings.Infrastructure.Data;
using Cephiro.Listings.Infrastructure.Data.Execute;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cephiro.Listings.Application.Catalog.Commands;



using Cephiro.Listings.Presentation.Api.Catalog.Endpoints;
using MassTransit;


namespace Cephiro.Listings.Infrastructure;

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
        services.AddSingleton<DapperConfig>();
    
        services.Configure<DapperConfig>(
        configuration.GetSection(DapperConfig.SectionName));


        return services;
    }

    public static IServiceCollection AddEfDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<ListingsDbContext>(options => 
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseNpgsql(configuration.GetConnectionString("ListingsConnection"),
            builder => builder.MigrationsAssembly(typeof(ListingsDbContext).Assembly.FullName)
        ));

        return services;
    }

    public static IServiceCollection AddWriteRepositories(this IServiceCollection services)
    {
        services.AddTransient<ICatalogExecute, CatalogExecute>();
        
        return services;
    }
}