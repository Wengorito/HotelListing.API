using Microsoft.EntityFrameworkCore;

namespace HotelListing.API
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
