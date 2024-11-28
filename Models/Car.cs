using CarRental_System.Filters;
using System.ComponentModel.DataAnnotations;

namespace CarRental_System.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Manufacturer Field is Required")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model Name is Required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Date of Manufacture is Required")]
        [Validate]
        public DateTime Year { get; set; }

        [Required(ErrorMessage = "Price Per Day is Required")]
        [Range(100, 10000, ErrorMessage = "Price Per Day must be between Rs. 100 and Rs. 10000")]
        public decimal PricePerDay { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
