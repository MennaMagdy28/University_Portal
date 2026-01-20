using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    public interface IStudentRegistrationRepository
    {
        Task<bool> HasPassedPrerequisiteAsync(Guid studentId, Guid courseId);
        Task<bool> IsAlreadyEnrolledAsync(Guid studentId, Guid courseId);
        Task<int> GetStudentLevelAsync(Guid studentId);
        Task<List<Guid>> GetCompletedCoursesAsync(Guid studentId);
        Task<bool> CheckTimeConflictAsync(Guid studentId, Guid groupId);
        Task<bool> CheckCourseCapacityAsync(Guid courseId, int maxStudentsPerCourse);
        Task<Dictionary<Guid, int>> GetCourseEnrollmentCountsAsync(List<Guid> courseIds);
        Task<List<Course>> GetPrerequisiteChainAsync(Guid courseId);
    }
}
