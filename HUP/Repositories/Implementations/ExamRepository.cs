using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;

namespace HUP.Repositories.Implementations
{
    public class ExamRepository : IExamRepository
    {
        private readonly HupDbContext _context;
        public ExamRepository(HupDbContext context)
        {
            _context = context;
        }
        public Task AddAsync(Exam entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exam>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Exam> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
