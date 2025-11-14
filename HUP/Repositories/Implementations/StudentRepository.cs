using HUP.Core.Entities.Academics;
using HUP.Core.Enums;
using HUP.Data;
using HUP.Repositories.Interfaces;

namespace HUP.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly HUPDbContext _context;
        public StudentRepository(HUPDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Student entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetByFacultyAsync(Guid facultyId)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetByDepartmentAsync(Guid departmentId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SoftDelete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Student entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAcademicStatusAsync(Guid studentId, AcademicStatus status)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCGPAAsync(Guid studentId, decimal cgpa)
        {
            throw new NotImplementedException();
        }
    }
}
