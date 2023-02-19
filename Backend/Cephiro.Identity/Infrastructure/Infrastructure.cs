using Cephiro.Identity.Application.Authentication.Commands;
using Cephiro.Identity.Application.Authentication.Queries;
using Cephiro.Identity.Application.Profile.Commands;
using Cephiro.Identity.Application.Profile.Queries;
using Cephiro.Identity.Application.Reporting.Commands;
using Cephiro.Identity.Application.Reporting.Queries;
using Cephiro.Identity.Infrastructure.Accessors;
using Cephiro.Identity.Infrastructure.Data;
using Cephiro.Identity.Infrastructure.Data.Access;
using Cephiro.Identity.Infrastructure.Executors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cephiro.Identity.Infrastructure;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDapperConnection(configuration);
        services.AddEfDbContext(configuration);
        
        services.AddWriteRepositories();
        services.AddReadRepositories();
        
        return services;
    }

    public static IServiceCollection AddDapperConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DapperConfig>();

        var DapperSettings = new DapperConfig();
        services.AddOptions<DapperConfig>().Configure<IConfiguration>((options, configuration)=>
            configuration.GetSection(DapperConfig.SectionName).Bind(options));

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
        services.AddScoped<IAuthExecute, AuthExecute>();
        services.AddScoped<IProfileExecute, ProfileExecute>();
        services.AddScoped<IReportExecute, ReportExecute>();

        return services;
    }

    public static IServiceCollection AddReadRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthAccess, AuthAccess>();
        services.AddScoped<IReportAccess, ReportAccess>();
        services.AddScoped<IProfileAccess, ProfileAccess>();
        
        return services;
    }
}