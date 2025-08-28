using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Infrastructure.Persistence;
using OfflineTicketing.Infrastructure.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. DbContext با SQLite
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
     

        // 2. Repositoryها
        services.AddScoped<ITicketRepository, TicketRepository>();

        return services;
    }
}
