using System.Text.Json.Serialization;

namespace CarRental.Models
{
    public class CarFeature
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public bool Navigation { get; set; }
        public string InternalRoofType { get; set; }
        public bool CruiseControl { get; set; }
        public bool HeatedSeats { get; set; }
        public bool CooledSeats { get; set; }
        public bool ElectricSeats { get; set; }
        public bool TintedWindows { get; set; }


        public Car Car { get; set; } 
}

