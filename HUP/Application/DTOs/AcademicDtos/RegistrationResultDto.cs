namespace HUP.Application.DTOs.AcademicDtos
{
    public class RegistrationResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Guid> RegisteredGroupIds { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; internal set; }
    }
}
