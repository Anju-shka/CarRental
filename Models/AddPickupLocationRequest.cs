using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class AddPickupLocationRequest
    {
        public int Id { get; set; }
        public string LocationAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<int> CarRentIds { get; set; }
    }
}
