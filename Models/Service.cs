using System.Text.Json.Serialization;

namespace CarRental.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int CarId { get; set; } 
        public string ServiceType { get; set; }
        public DateTime ServiceDate { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }


        public Car Car { get; set; }
    }
}
