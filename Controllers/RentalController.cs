using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    [Route("api/RentDTO")]
    [ApiController]
    public class CarRentalDTOController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarRentalDTOController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RentDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarRentDto>>> GetRents()
        {
            var rents = await _context.CarRents
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .Include(r => r.PickupLocation)
                .Select(r => new CarRentDto
                {
                    Id = r.Id,
                    CarId = r.CarId,
                    CustomerId = r.CustomerId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    PickupLocationId = r.PickupLocationId
                }).ToListAsync();

            return Ok(rents);
        }

        // GET: api/RentDTO/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarRentDto>> GetRent(int id)
        {
            var rent = await _context.CarRents
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .Include(r => r.PickupLocation)
                .Select(r => new CarRentDto
                {
                    Id = r.Id,
                    CarId = r.CarId,
                    CustomerId = r.CustomerId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    PickupLocationId = r.PickupLocationId
                })
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rent == null)
            {
                return NotFound();
            }

            return rent;
        }

        // POST: api/RentDTO
        [HttpPost]
        public async Task<ActionResult<CarRentDto>> PostRent(AddRentRequest request)
        {
            if (request == null)
            {
                return BadRequest("Rent object is null.");
            }

            var car = await _context.Cars.FindAsync(request.CarId);
            if (car == null) return BadRequest("Invalid CarId");

            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null) return BadRequest("Invalid CustomerId");

            var pickupLocation = await _context.PickupLocations.FindAsync(request.PickupLocationId);
            if (pickupLocation == null) return BadRequest("Invalid PickupLocationId");

            var rent = new CarRent
            {
                CarId = request.CarId,
                CustomerId = request.CustomerId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PickupLocationId = request.PickupLocationId
            };

            _context.CarRents.Add(rent);
            await _context.SaveChangesAsync();

            var rentDto = new CarRentDto
            {
                Id = rent.Id,
                CarId = rent.CarId,
                CustomerId = rent.CustomerId,
                StartDate = rent.StartDate,
                EndDate = rent.EndDate,
                PickupLocationId = rent.PickupLocationId
            };

            return CreatedAtAction(nameof(GetRent), new { id = rent.Id }, rentDto);
        }

        // PUT: api/RentDTO/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRent(int id, AddRentRequest request)
        {
            if (id != request.CarId)
            {
                return BadRequest("Rent ID mismatch.");
            }

            var existingRent = await _context.CarRents.FindAsync(id);
            if (existingRent == null)
            {
                return NotFound();
            }

            existingRent.CarId = request.CarId;
            existingRent.CustomerId = request.CustomerId;
            existingRent.StartDate = request.StartDate;
            existingRent.EndDate = request.EndDate;
            existingRent.PickupLocationId = request.PickupLocationId;

            _context.Entry(existingRent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CarRents.Any(r => r.Id == id))
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

        // DELETE: api/RentDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRent(int id)
        {
            var rent = await _context.CarRents.FindAsync(id);
            if (rent == null)
            {
                return NotFound();
            }

            _context.CarRents.Remove(rent);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
