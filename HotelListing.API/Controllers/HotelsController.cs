using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Exceptions;
using HotelListing.API.Models;
using HotelListing.API.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHotelsRepository _hotelsRepository;

        public HotelsController(IMapper mapper, IHotelsRepository hotelsRepository)
        {
            _mapper = mapper;
            _hotelsRepository = hotelsRepository;
        }

        // GET: api/Hotels/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await _hotelsRepository.GetAllAsync<HotelDto>();
            return Ok(hotels);
        }

        // GET: api/Hotels/?PageSize=5&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<PagedResult<HotelDto>>> GetPagedHotels([FromQuery] QueryParameters queryParameters)
        {
            return Ok(await _hotelsRepository.GetAllAsync<HotelDto>(queryParameters));
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync<HotelDto>(id);
            return Ok(hotel);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDto hotelDto)
        {
            if (id != hotelDto.Id)
            {
                throw new BadRequestException(nameof(PutHotel), id);
            }

            try
            {
                await _hotelsRepository.UpdateAsync(id, hotelDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExistsAsync(id))
                {
                    throw new NotFoundException(nameof(PutHotel), id);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelDto>> PostHotel(CreateHotelDto createHotelDto)
        {
            var hotel = await _hotelsRepository.AddAsync<CreateHotelDto, HotelDto>(createHotelDto);

            return CreatedAtAction(nameof(PostHotel), new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _hotelsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExistsAsync(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
