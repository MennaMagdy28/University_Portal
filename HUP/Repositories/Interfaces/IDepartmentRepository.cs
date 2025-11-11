using HUP.Core.Entities.AcademicModels;

namespace HUP.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> GetByIdAsync(int id);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<IEnumerable<Department>> GetByFacultyIdAsync(int facultyId);
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(int id);
    }
}
