using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class PickupLocation
    {
        public int Id { get; set; }
        public string LocationAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<CarRent> CarRents { get; set; }
    }
}
