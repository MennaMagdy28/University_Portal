namespace HUP.Application.DTOs.IdentityDtos;

public class ProfileStatus
{
    public bool PasswordExpired { get; set; }
    // contact or personal information is missing
    public bool ProfileIncomplete { get; set; }
    public List<string?> MissingFields { get; set; }
}