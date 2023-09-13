using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelListing.API.Data
{
    public class HotelListingDbContextFactory : IDesignTimeDbContextFactory<HotelListingDbContext>
    {
        // After creating a separate Data Project scaffoling controllers with EF stopped working.
        // This is a fix
        public HotelListingDbContext CreateDbContext(string[] args)
        {
            // get the environment variable for the different runtime
            // and inject it there
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<HotelListingDbContext>();
            var conn = config.GetConnectionString("HotelListingDbConnectionString");
            optionsBuilder.UseSqlServer(conn);
            return new HotelListingDbContext(optionsBuilder.Options);
        }
    }
}
