using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    public interface IClassGroupRepository : IGenericRepository<ClassGroup>
    {
        Task<IEnumerable<ClassGroup>> GetByCourseOfferingAsync(Guid courseOfferingId);
        Task<IEnumerable<ClassGroup>> GetAvailableForStudentAsync(Guid studentId, int studentLevel);
        Task<bool> IsGroupFullAsync(Guid groupId);
        Task<int> GetGroupEnrollmentCountAsync(Guid groupId);
        Task IncrementEnrollmentAsync(Guid groupId);
        Task DecrementEnrollmentAsync(Guid groupId);
        Task<IEnumerable<ClassGroup>> GetByStudentAsync(Guid studentId);
        Task<IEnumerable<ClassGroup>> GetByInstructorAsync(Guid instructorId);
        Task<bool> CheckTimeConflictAsync(Guid studentId, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime, Guid? excludeGroupId = null);
    }
}
