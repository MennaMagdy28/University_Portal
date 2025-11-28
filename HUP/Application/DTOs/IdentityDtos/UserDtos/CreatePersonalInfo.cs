using HUP.Core.Enums;

namespace HUP.Application.DTOs.IdentityDtos.UserDtos;

public class CreatePersonalInfo
{
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string Religion { get; set; }
    public string Nationality { get; set; }
    public string BirthPlace { get; set; }
    public string? FullEnglishName { get; set; }
}