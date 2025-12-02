using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class ExamRepository : IExamRepository
    {
        private readonly HupDbContext _context;
        public ExamRepository(HupDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Exam exam)
        {
            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Exam>> GetAllAsync()
        {
            return await _context.Exams.ToListAsync();
        }

        public Task<Exam> GetByIdAsync(Guid id)
        {
            var exam = _context.Exams.Include(e => e.CourseOffering)
                .ThenInclude(c => c.Department)
                .ThenInclude(d => d.Instructors)
                .ThenInclude(i => i.User)
                .FirstOrDefaultAsync(e => e.Id == id);
            return exam;
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await _context.Exams.FindAsync(id);

            if (entity != null)
                _context.Exams.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<Exam>> GetByCoursesAsync(List<Guid> courseIds)
        {
            if (!courseIds.Any())
                return new List<Exam>();

            return await _context.Exams
                .Include(e => e.CourseOffering)
                .ThenInclude(c => c.Department)
                .ThenInclude(d => d.Instructors)
                .ThenInclude(i => i.User)
                .Where(e => courseIds.Contains(e.CourseOffering.CourseId))
                .OrderBy(e => e.ExamDate)
                .ThenBy(e => e.ExamTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Exam>> GetAllActiveAsync()
        {
            return await _context.Exams
                .Include(e => e.CourseOffering)
                .ThenInclude(c => c.Department)
                .ThenInclude(d => d.Instructors)
                .ThenInclude(i => i.User)
                .OrderBy(e => e.ExamDate)
                .ThenBy(e => e.ExamTime)
                .ToListAsync();
        }

        public async Task UpdateAsync(Exam exam)
        {
            exam.UpdatedAt = DateTime.UtcNow;
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var exam = await GetByIdAsync(id);
            if (exam != null)
            {
                exam.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(exam);
            }
        }
    }
}
