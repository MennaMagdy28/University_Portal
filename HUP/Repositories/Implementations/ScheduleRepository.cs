using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class ScheduleRepository : IScheduleRepository
    {

        private readonly HUPDbContext _context;
        public ScheduleRepository(HUPDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Schedule entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Schedule> GetByIdAsync(Guid id)
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

        public void Update(Schedule entity)
        {
            throw new NotImplementedException();
        }
    }
}
