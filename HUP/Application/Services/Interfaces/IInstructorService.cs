using HUP.Core.Entities.Academics;

namespace HUP.Application.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<Instructor> GetByIdAsync(Guid id);
        Task<IEnumerable<Instructor>> GetByDepartmentAsync(Guid departmentId);
        Task<IEnumerable<Course>> GetAssignedCoursesAsync(Guid instructorId, Guid semesterId);
        Task<IEnumerable<ClassGroup>> GetScheduleAsync(Guid instructorId, Guid semesterId);
    }
}
