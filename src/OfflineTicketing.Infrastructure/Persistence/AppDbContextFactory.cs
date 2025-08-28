using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace OfflineTicketing.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // مسیر پروژه برای خواندن appsettings.json
            var config = new ConfigurationBuilder()
         .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OfflineTicketing.Api")) // مسیر پروژه API
         .AddJsonFile("appsettings.json", optional: false)
         .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlite(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
