using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    public interface ICourseScheduleRepository
    {
        Task<CourseSchedule> GetByIdAsync(Guid id);
        Task<IEnumerable<CourseSchedule>> GetByStudentAsync(Guid studentId);
        Task AddAsync(CourseSchedule schedule);
        Task UpdateAsync(CourseSchedule schedule);
        Task DeleteAsync(Guid id);
    }
}
