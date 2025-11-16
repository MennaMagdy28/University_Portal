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

        public async Task<Schedule> GetByCourseOfferingIdAndGroupAsync(Guid courseOfferingId, string group)
        {
            var lecture = await _context.Schedules
                  .Where(s => s.CourseOfferingId == courseOfferingId &&
                         s.Group == group &&
                         s.CourseOffering.Semester.IsActive)
                  .Include(s => s.CourseOffering)
                  .ThenInclude(co => co.Course)
                  .Include(s => s.CourseOffering)
                  .ThenInclude(co => co.Instructor).FirstOrDefaultAsync();
            return lecture;
        }

        public async Task AddAsync(Schedule entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Schedules.AddAsync(entity);
        }
        public async Task<IEnumerable<Schedule>> GetAllAsync()
        {
            return await _context.Schedules.ToListAsync();
        }

        public async Task<Schedule> GetByIdAsync(Guid id)
        {
            var s = await _context.Schedules.FindAsync(id);
            return s;
        }

        public void Remove(Guid id)
        {
            var entity = _context.Schedules.Find(id);
            if (entity != null)
            {
                _context.Schedules.Remove(entity);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SoftDelete(Guid id)
        {
            var entity = _context.Schedules.Find(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;
                _context.Schedules.Update(entity);
            }
        }

        public void Update(Schedule entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Schedules.Update(entity);
        }

  
    }
}
