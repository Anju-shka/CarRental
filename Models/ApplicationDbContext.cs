using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CarRental.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarRent> CarRents { get; set; } 
        public DbSet<Service> Services { get; set; }
        public DbSet<CarFeature> CarFeatures { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PickupLocation> PickupLocations { get; set; }

    }
}
