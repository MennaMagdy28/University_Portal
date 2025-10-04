using System.ComponentModel.DataAnnotations;

namespace HUP.Core.DTOs.UserDtos
{
    public class LoginDto
    {
        [Required]
        public string NationalID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
