using CarRental_System.Filters;
using System.ComponentModel.DataAnnotations;

namespace CarRental_System.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Name Length should be between 5 and 100.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Name should have only alphabets, numbers and spaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        [UniqueEmail]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Name Length should be between 8 and 100.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Name should have only alphabets, numbers and underscore.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; } = "User";
        public int? CarId { get; set; }
    }
}
