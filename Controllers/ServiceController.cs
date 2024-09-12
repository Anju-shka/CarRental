using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    [Route("api/Service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }
        // POST: api/Service
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(AddServiceRequest request)
        {
            if (request == null)
            {
                return BadRequest("Service object is null.");
            }

          
            Console.WriteLine($"Received POST request with CarId: {request.CarId}, ServiceType: {request.ServiceType}, ServiceDate: {request.ServiceDate}, Cost: {request.Cost}, Description: {request.Description}");

            var car = await _context.Cars.FindAsync(request.CarId);
            if (car == null)
            {
                return BadRequest($"Invalid CarId: {request.CarId}. Car not found in the database.");
            }

            var service = new Service
            {
                CarId = request.CarId,
                ServiceType = request.ServiceType,
                ServiceDate = request.ServiceDate,
                Cost = request.Cost,
                Description = request.Description
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }


        // GET: api/Service/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Service/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, AddServiceRequest request)
        {
            if (id != request.CarId)
            {
                return BadRequest("Service ID mismatch.");
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            service.ServiceType = request.ServiceType;
            service.ServiceDate = request.ServiceDate;
            service.Cost = request.Cost;
            service.Description = request.Description;

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // DELETE: api/Service/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
