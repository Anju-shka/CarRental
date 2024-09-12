using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    [Route("api/PickupLocation")]
    [ApiController]
    public class PickupLocationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PickupLocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PickupLocation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PickupLocation>>> GetPickupLocations()
        {
            return await _context.PickupLocations.ToListAsync();
        }

        // GET: api/PickupLocation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PickupLocation>> GetPickupLocation(int id)
        {
            var pickupLocation = await _context.PickupLocations.FindAsync(id);

            if (pickupLocation == null)
            {
                return NotFound();
            }

            return pickupLocation;
        }

        // POST: api/PickupLocation
        [HttpPost]
        public async Task<ActionResult<PickupLocation>> PostPickupLocation(AddPickupLocationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pickupLocation = new PickupLocation
            {
                LocationAddress = request.LocationAddress,
                City = request.City,
                Country = request.Country,
                CarRents = request.CarRentIds != null
                    ? await _context.CarRents.Where(cr => request.CarRentIds.Contains(cr.Id)).ToListAsync()
                    : new List<CarRent>()
            };

            _context.PickupLocations.Add(pickupLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPickupLocation), new { id = pickupLocation.Id }, pickupLocation);
        }

        // PUT: api/PickupLocation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPickupLocation(int id, AddPickupLocationRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pickupLocation = await _context.PickupLocations
                                               .Include(pl => pl.CarRents)
                                               .FirstOrDefaultAsync(pl => pl.Id == id);

            if (pickupLocation == null)
            {
                return NotFound();
            }

          
            pickupLocation.LocationAddress = request.LocationAddress;
            pickupLocation.City = request.City;
            pickupLocation.Country = request.Country;

          
            pickupLocation.CarRents.Clear(); 
            if (request.CarRentIds != null)
            {
                foreach (var carRentId in request.CarRentIds)
                {
                    var carRent = await _context.CarRents.FindAsync(carRentId);
                    if (carRent != null)
                    {
                        pickupLocation.CarRents.Add(carRent); 
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PickupLocationExists(id))
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

        // DELETE: api/PickupLocation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePickupLocation(int id)
        {
            var pickupLocation = await _context.PickupLocations.FindAsync(id);

            if (pickupLocation == null)
            {
                return NotFound();
            }

            _context.PickupLocations.Remove(pickupLocation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PickupLocationExists(int id)
        {
            return _context.PickupLocations.Any(e => e.Id == id);
        }
    }
}
