using System.ComponentModel.DataAnnotations;

namespace HUP.Application.DTOs.AuthDtos;

public class UpdatePassword
{
    [Required]
    public string CurrentPassword { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string NewPassword { get; set; }

    [Required]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match.")] // <--- The Magic Line
    public string ConfirmNewPassword { get; set; }

}