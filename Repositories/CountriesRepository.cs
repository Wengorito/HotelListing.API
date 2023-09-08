using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repositories
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _context;

        public CountriesRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Country> GetDetailsAsync(int id)
        {
            var country = await _context.Countries
                .Include(q => q.Hotels)
                .FirstOrDefaultAsync(q => q.Id == id);

            return country;
        }
    }
}
