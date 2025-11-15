using HUP.Repositories.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Data;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class CourseOfferingRepository : ICourseOfferingRepository
    {
        private readonly HUPDbContext _context;
        public CourseOfferingRepository(HUPDbContext context)
        {
            _context = context;
        }
        //public async Task<IEnumerable<CourseOffering>> GetActiveCourseOfferingAsync(Guid DepartmentId, Guid SemesterId)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<IEnumerable<CourseOffering>> GetAvailbleToRegisterAsync(Guid DepartmentId, Guid SemesterId)
        {
           throw new NotImplementedException();
        }

        public async Task AddAsync(CourseOffering entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.CourseOfferings.AddAsync(entity);
        }

        public async Task<IEnumerable<CourseOffering>> GetAllAsync()
        {
            return await _context.CourseOfferings
                .Include(co => co.Course)
                .Include(co => co.Instructor)
                .Include(co => co.Semester)
                .ToListAsync();
        }

        public async Task<CourseOffering> GetByIdAsync(Guid id)
        {
            var co = await _context.CourseOfferings
                .Include(co => co.Course)
                .Include(co => co.Instructor)
                .Include(co => co.Semester)
                .FirstOrDefaultAsync(co => co.Id == id);
            return co;
        }

        public void Remove(Guid courseId)
        {
           _context.CourseOfferings.Remove(new CourseOffering { Id = courseId });
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SoftDelete(Guid courseId)
        {
            var courseOffering = _context.CourseOfferings.Find(courseId);
            if (courseOffering != null)
            {
                courseOffering.IsDeleted = true;
                _context.CourseOfferings.Update(courseOffering);
            }
        }

        public void Update(CourseOffering entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.CourseOfferings.Update(entity);
        }
    }
}
