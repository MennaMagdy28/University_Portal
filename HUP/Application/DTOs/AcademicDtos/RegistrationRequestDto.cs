namespace HUP.Application.DTOs.AcademicDtos
{
    public class RegistrationRequestDto
    {
        public Guid StudentId { get; set; }
        public List<Guid> SelectedGroupIds { get; set; }
    }
}
