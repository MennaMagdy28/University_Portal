namespace HUP.Application.DTOs.AcademicDtos
{
    public class UpdateClassGroupDto : CreateClassGroupDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
