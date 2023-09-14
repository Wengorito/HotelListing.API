using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Exceptions;
using HotelListing.API.Models.Country;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Controllers
{
    [Route("api/v{version:apiVersion}/countries")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CountriesV2Controller : ControllerBase
    {
        private readonly ILogger<CountriesV2Controller> _logger;
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countries;

        public CountriesV2Controller(ILogger<CountriesV2Controller> logger, IMapper mapper, ICountriesRepository countries)
        {
            _logger = logger;
            _mapper = mapper;
            _countries = countries;
        }

        // GET: api/Countries
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countries.GetAllAsync<GetCountryDto>();
            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countries.GetDetailsAsync(id);
            return Ok(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            // to też można przenieść do repozytorium, zależnie od przyjętej konwencji
            if (id != updateCountryDto.Id)
            {
                throw new BadRequestException(nameof(PutCountry), id);
            }

            try
            {
                await _countries.UpdateAsync<UpdateCountryDto>(id, updateCountryDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _countries.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CountryDto>> PostCountry(CreateCountryDto createCountryDto)
        {
            var country = await _countries.AddAsync<CreateCountryDto, CountryDto>(createCountryDto);

            // TODO check for return type. No visible differences. CountryDetailDto then?
            return CreatedAtAction(nameof(PostCountry), new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _countries.DeleteAsync(id);

            return NoContent();
        }
    }
}
