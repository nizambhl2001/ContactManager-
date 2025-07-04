using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AspNetCoreMvcJwtApp.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Configuration load from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // project root directory
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
