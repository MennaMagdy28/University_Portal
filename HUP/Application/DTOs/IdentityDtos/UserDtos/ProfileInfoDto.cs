namespace HUP.Application.DTOs.IdentityDtos.UserDtos;

public class ProfileInfoDto
{
    public PersonalInfoDto PersonalInfo { get; set; }
    public ContactInfoDto ContactInfo { get; set; }
    public string NationalId { get; set; }
    public string? Email { get; set; }
    public string FullName { get; set; }
}