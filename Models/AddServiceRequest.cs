using System.ComponentModel.DataAnnotations;
namespace CarRental.Models
{

    public class AddServiceRequest
    {
        public int CarId { get; set; }
        public string ServiceType { get; set; }
        public DateTime ServiceDate { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
    }
}
