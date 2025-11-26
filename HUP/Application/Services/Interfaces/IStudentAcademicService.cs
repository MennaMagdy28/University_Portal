using HUP.Application.DTOs.AcademicDtos;

namespace HUP.Application.Services.Interfaces
{
    public interface IStudentAcademicService
    {
        Task<IEnumerable<StudentTimetableDto>> GetStudentTimetableAsync(Guid studentId);
        Task<IEnumerable<StudentExamScheduleDto>> GetStudentExamScheduleAsync(Guid studentId);
    }
}
