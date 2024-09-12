namespace CarRental.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int RegisteredYear { get; set; }
        public string LicencePlate { get; set; }
        public int NumberOfSeats { get; set; }
        public string EngineType { get; set; }
        public int EngineDisplacement { get; set; }
        public string CarClass { get; set; }
        public string Transmission { get; set; }
        public string ExteriorColor { get; set; }
        public string InteriorColor { get; set; }
        public string InteriorType { get; set; }
        public string Drivetrain { get; set; }
        public int Kilometers { get; set; }

        public CarFeature CarFeatures { get; set; } 
        public ICollection<CarRent> CarRents { get; set; }
        public ICollection<Service> Services { get; set; }
    }

}

