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
        public async Task AddAsync(Exam entity)
        {
            await _context.Exams.AddAsync(entity);
        }

        public async Task<IEnumerable<Exam>> GetAllAsync()
        {
            return await _context.Exams.ToListAsync();
        }

        public Task<Exam> GetByIdAsync(Guid id)
        {
            var exam = _context.Exams.FirstOrDefaultAsync(e => e.Id == id);
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
    }
}
