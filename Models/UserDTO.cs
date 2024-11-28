using System.ComponentModel.DataAnnotations;

namespace CarRental_System.Models
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
