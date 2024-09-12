using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class CarRent
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PickupLocationId { get; set; }

        public Car Car { get; set; }

        public Customer Customer { get; set; }

        public PickupLocation PickupLocation { get; set; }
    }
}
