using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    public interface IInstructorRepository : IGenericRepository<Instructor>
    {
        Task<IEnumerable<Instructor>> GetByDepartmentAsync(Guid departmentId);
        Task<IEnumerable<Instructor>> GetByFacultyAsync(Guid facultyId);
        Task<Instructor> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Instructor>> GetAvailableInstructorsAsync(Guid courseId, Guid semesterId);
        Task<bool> IsInstructorAvailableAsync(Guid instructorId, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime);
        Task<IEnumerable<Course>> GetAssignedCoursesAsync(Guid instructorId, Guid semesterId);
        Task<IEnumerable<ClassGroup>> GetInstructorScheduleAsync(Guid instructorId, Guid semesterId);
    }
}
