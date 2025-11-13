using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<IEnumerable<Course>> GetByDepartmentIdAsync(Guid departmentId);
    }
}
