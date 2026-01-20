using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        //to discuss (q: course might be available for many departments at the same faculty => University Requirements & Faculty Requirements)
        Task<IEnumerable<Course>> GetCoursesByLevelAsync(int level);
        Task<IEnumerable<Course>> GetCoursesByDepartmentAsync(Guid departmentId);
        Task<IEnumerable<Course>> GetCoursesByIdsAsync(List<Guid> courseIds);
        Task<IEnumerable<Course>> GetCurrentOfferingsAsync(Guid courseId, Guid departmentId);
        Task<IEnumerable<Course>> GetPrerequisitesAsync(Guid courseId);
    }
}
