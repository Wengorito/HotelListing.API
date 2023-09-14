using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Exceptions;
using HotelListing.API.Models;
using HotelListing.API.Models.Country;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Controllers
{
    [Route("api/v{version:apiVersion}/countries")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)] // deprecated only for an example
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController(ILogger<CountriesController> logger, IMapper mapper, ICountriesRepository countries)
        {
            _logger = logger;
            _mapper = mapper;
            _countriesRepository = countries;
        }

        //// GET: api/Countries/GetAll
        //[HttpGet("GetAll")]
        //public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        //{
        //    var countries = await _countriesRepository.GetAllAsync();
        //    return Ok(_mapper.Map<List<GetCountryDto>>(countries));
        //}

        // GET: /api/v1/Countries/?PageSize=5&PageNumber=1
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<PagedResult<GetCountryDto>>> GetPagedCountries([FromQuery] QueryParameters queryParameters)
        {
            var pagedCountriesResult = await _countriesRepository.GetAllAsync<GetCountryDto>(queryParameters);

            return Ok(pagedCountriesResult);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetailsAsync(id);

            return Ok(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                throw new BadRequestException(nameof(PutCountry), id);
            }

            try
            {
                await _countriesRepository.UpdateAsync(id, updateCountryDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _countriesRepository.Exists(id))
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
            var country = await _countriesRepository.AddAsync<CreateCountryDto, CountryDto>(createCountryDto);

            // TODO check for return type. No visible differences. CountryDetailDto then?
            return CreatedAtAction(nameof(PostCountry), new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
