using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripLogServer.Infrastructure.Context;

namespace TripLogServer.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        return services;
    }
}