namespace CarRental.Models
{
    public class AddCarRequest
    {
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
        public CarFeatureRequest CarFeature { get; set; }
    }
}
