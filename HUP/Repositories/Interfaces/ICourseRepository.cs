using HUP.Core.Entities.AcademicModels;

namespace HUP.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course> GetByIdAsync(int id);
        Task<IEnumerable<Course>> GetAllAsync();
        Task<IEnumerable<Course>> GetByDepartmentIdAsync(int departmentId);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
    }
}
