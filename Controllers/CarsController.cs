using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Core;

namespace CarRental.Controllers
{
    [Route("api/CarRental")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CarRental
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars
                                 .Include(c => c.CarFeatures)
                                 .Include(c => c.CarRents) 
                                 .Include(c => c.Services)
                                 .ToListAsync();
        }

        // GET: api/CarRental/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars
                                    .Include(c => c.CarFeatures)
                                    .Include(c => c.CarRents) 
                                    .Include(c => c.Services)
                                    .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // POST: api/CarRental
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(AddCarRequest request)
        {
            if (request == null)
            {
                return BadRequest("Car object is null.");
            }

            var car = new Car()
            {
                Brand = request.Brand,
                Model = request.Model,
                RegisteredYear = request.RegisteredYear,
                LicencePlate = request.LicencePlate,
                NumberOfSeats = request.NumberOfSeats,
                EngineType = request.EngineType,
                EngineDisplacement = request.EngineDisplacement,
                CarClass = request.CarClass,
                Transmission = request.Transmission,
                ExteriorColor = request.ExteriorColor,
                InteriorColor = request.InteriorColor,
                InteriorType = request.InteriorType,
                Drivetrain = request.Drivetrain,
                Kilometers = request.Kilometers,
                CarFeatures = new CarFeature() 
                {
                    Navigation = request.CarFeature.Navigation,
                    InternalRoofType = request.CarFeature.InternalRoofType,
                    CruiseControl = request.CarFeature.CruiseControl,
                    HeatedSeats = request.CarFeature.HeatedSeats,
                    CooledSeats = request.CarFeature.CooledSeats,
                    ElectricSeats = request.CarFeature.ElectricSeats,
                    TintedWindows = request.CarFeature.TintedWindows,
                }
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

      // PUT: api/CarRental/5
[HttpPut("{id}")]
public async Task<IActionResult> PutCar(int id, AddCarRequest request)
{
    
    var existingCar = await _context.Cars
                                    .Include(c => c.CarFeatures)
                                    .Include(c => c.CarRents)
                                    .Include(c => c.Services)
                                    .FirstOrDefaultAsync(c => c.Id == id);

    if (existingCar == null)
    {
        return NotFound();
    }


    existingCar.Brand = request.Brand;
    existingCar.Model = request.Model;
    existingCar.RegisteredYear = request.RegisteredYear;
    existingCar.LicencePlate = request.LicencePlate;
    existingCar.NumberOfSeats = request.NumberOfSeats;
    existingCar.EngineType = request.EngineType;
    existingCar.EngineDisplacement = request.EngineDisplacement;
    existingCar.CarClass = request.CarClass;
    existingCar.Transmission = request.Transmission;
    existingCar.ExteriorColor = request.ExteriorColor;
    existingCar.InteriorColor = request.InteriorColor;
    existingCar.InteriorType = request.InteriorType;
    existingCar.Drivetrain = request.Drivetrain;
    existingCar.Kilometers = request.Kilometers;


    if (existingCar.CarFeatures != null)
    {
        existingCar.CarFeatures.Navigation = request.CarFeature.Navigation;
        existingCar.CarFeatures.InternalRoofType = request.CarFeature.InternalRoofType;
        existingCar.CarFeatures.CruiseControl = request.CarFeature.CruiseControl;
        existingCar.CarFeatures.HeatedSeats = request.CarFeature.HeatedSeats;
        existingCar.CarFeatures.CooledSeats = request.CarFeature.CooledSeats;
        existingCar.CarFeatures.ElectricSeats = request.CarFeature.ElectricSeats;
        existingCar.CarFeatures.TintedWindows = request.CarFeature.TintedWindows;
    }

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!CarExists(id))
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


        // DELETE: api/CarRental/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
          
            Console.WriteLine($"Attempting to delete car with ID: {id}");

            var car = await _context.Cars
                                    .Include(c => c.CarFeatures)
                                    .Include(c => c.CarRents)
                                    .Include(c => c.Services)
                                    .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                Console.WriteLine("Car not found");
                return NotFound();
            }

            if (car.CarFeatures != null)
            {
                _context.CarFeatures.Remove(car.CarFeatures);
            }

            if (car.CarRents != null && car.CarRents.Any())
            {
                _context.CarRents.RemoveRange(car.CarRents);
            }

            if (car.Services != null && car.Services.Any())
            {
                _context.Services.RemoveRange(car.Services);
            }

            _context.Cars.Remove(car);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }


    }
}
