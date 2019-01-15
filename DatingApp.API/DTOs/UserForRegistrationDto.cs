using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class UserForRegistrationDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Your password length must be greater than 8 characters!")]
        public string Password {get; set;}
    }
}