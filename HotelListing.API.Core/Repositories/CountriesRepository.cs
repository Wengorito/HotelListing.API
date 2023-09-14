using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Exceptions;
using HotelListing.API.Models.Country;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repositories
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public CountriesRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountryDto> GetDetailsAsync(int id)
        {
            var country = await _context.Countries
                .Include(q => q.Hotels)
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetDetailsAsync), id);
            }

            return country;
        }
    }
}
