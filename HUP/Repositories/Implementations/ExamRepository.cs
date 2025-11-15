using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class ExamRepository : IExamRepository
    {
        private readonly HUPDbContext _context;
        public ExamRepository(HUPDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Exam entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
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

        public void Remove(Guid id)
        {
            var entity = _context.Exams.Find(id);
            if (entity != null)
            {
                _context.Exams.Remove(entity);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SoftDelete(Guid id)
        {
            var entity = _context.Exams.Find(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;
                _context.Exams.Update(entity);
            }
        }

        public void Update(Exam entity)
        {
           entity.UpdatedAt = DateTime.UtcNow;
           _context.Exams.Update(entity);
        }
    }
}
