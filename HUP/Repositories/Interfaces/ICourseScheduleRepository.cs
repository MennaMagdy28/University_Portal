using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    public interface ICourseScheduleRepository
    {
        Task<IEnumerable<CourseSchedule>> GetByStudentAsync(Guid studentId);
    }
}
