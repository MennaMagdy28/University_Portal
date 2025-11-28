using HUP.Application.DTOs.AcademicDtos;

namespace HUP.Application.Services.Interfaces
{
    public interface ICourseScheduleService
    {
        Task<CourseScheduleDto> GetByIdAsync(Guid id);
        Task<IEnumerable<CourseScheduleDto>> GetAllAsync();
        Task<CourseScheduleDto> CreateAsync(CourseScheduleCreateDto scheduleDto);
        Task<CourseScheduleDto> UpdateAsync(Guid id, CourseScheduleUpdateDto scheduleDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
