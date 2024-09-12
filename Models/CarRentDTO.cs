namespace CarRental.Models
{
    public class CarRentDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PickupLocationId { get; set; }
    }

}
