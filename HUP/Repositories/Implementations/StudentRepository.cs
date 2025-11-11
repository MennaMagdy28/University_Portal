using HUP.Core.Enums;
using HUP.Core.Models.AcademicModels;
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

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Faculty)
                .Include(s => s.Program)
                .ThenInclude(p => p.Department)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> GetByUserIdAsync(int userId)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Faculty)
                .Include(s => s.Program)
                .FirstOrDefaultAsync(s => s.UserID == userId);
        }

        public async Task<Student> GetByUniversityCodeAsync(string universityCode)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.UniversityCode == universityCode);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Faculty)
                .Include(s => s.Program)
                .Where(s => s.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetByFacultyAsync(int facultyId)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Program)
                .Where(s => s.FacultyID == facultyId && s.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetByProgramAsync(int programId)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Faculty)
                .Where(s => s.ProgramID == programId && s.IsActive)
                .ToListAsync();
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            student.UpdatedAt = DateTime.UtcNow;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAcademicStatusAsync(int studentId, AcademicStatus status)
        {
            var student = await GetByIdAsync(studentId);
            if (student != null)
            {
                student.AcademicStatus = status;
                student.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(student);
            }
        }

        public async Task UpdateCGPAAsync(int studentId, decimal cgpa)
        {
            var student = await GetByIdAsync(studentId);
            if (student != null)
            {
                student.CGPA = cgpa;
                student.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(student);
            }
        }
    }
}
