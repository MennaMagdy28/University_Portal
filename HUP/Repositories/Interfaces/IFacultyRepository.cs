using HUP.Core.Models.AcademicModels;

namespace HUP.Repositories.Interfaces
{
    public interface IFacultyRepository
    {
        Task<Faculty> GetByIdAsync(int id);
        Task<IEnumerable<Faculty>> GetAllAsync();
        Task AddAsync(Faculty faculty);
        Task UpdateAsync(Faculty faculty);
        Task DeleteAsync(int id);
    }
}
