using HUP.Core.Enums;
using HUP.Core.Models.AcademicModels;

namespace HUP.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetByIdAsync(int id);
        Task<Student> GetByUserIdAsync(int userId);
        Task<Student> GetByUniversityCodeAsync(string universityCode);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<IEnumerable<Student>> GetByFacultyAsync(int facultyId);
        Task<IEnumerable<Student>> GetByProgramAsync(int programId);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task UpdateAcademicStatusAsync(int studentId, AcademicStatus status);
        Task UpdateCGPAAsync(int studentId, decimal cgpa);
    }
}
