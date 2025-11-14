using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;

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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Exam>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Exam> GetByIdAsync(Guid id)
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

        public void Update(Exam entity)
        {
            throw new NotImplementedException();
        }
    }
}
