using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.User
{
    public class ApiUserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your password must be between {2} and {1} characters long", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
