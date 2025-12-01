using HUP.Core.Entities.Identity;

namespace HUP.Application.DTOs.IdentityDtos.UserDtos;

public class CreateUserDto
{
    public string NationalId { get; set; }
    public string? Email { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }
    public Guid RoleId { get; set; }
    public CreatePersonalInfo PersonalInfo { get; set; }
    public CreateContactInfo ContactInfo { get; set; }
}