using HUP.Core.Entities.Academics;
using HUP.Core.Enums;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            await _context.Students.AddAsync(entity);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetByFacultyAsync(Guid facultyId)
        {
            return await _context.Students
                .Where(s => s.Department.FacultyID == facultyId)
                .ToListAsync();
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.UserId == id);
            return student;
        }

        public async Task<IEnumerable<Student>> GetByDepartmentAsync(Guid departmentId)
        {
            var students = await _context.Students
                .Where(s => s.DepartmentId == departmentId)
                .ToListAsync();
            return students;
        }

        public void Remove(Guid id)
        {
            var entity = _context.Students.Find(id);
            if (entity != null)
            {
                _context.Students.Remove(entity);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Student entity)
        {
            _context.Students.Update(entity);
        }

        public void UpdateAcademicStatusAsync(Guid studentId, AcademicStatus status)
        {
            _context.Students
                .Where(s => s.UserId == studentId)
                .ExecuteUpdate(s => s.SetProperty(st => st.AcademicStatus, status));
        }

        public void UpdateCGPAAsync(Guid studentId, decimal cgpa)
        {
            _context.Students
                .Where(s => s.UserId == studentId)
                .ExecuteUpdate(s => s.SetProperty(st => st.CGPA, cgpa));
        }
        public void SoftDelete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
