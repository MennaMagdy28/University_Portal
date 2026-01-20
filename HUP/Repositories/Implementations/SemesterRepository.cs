using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly HupDbContext _context;

        public SemesterRepository(HupDbContext context)
        {
            _context = context;
        }

        public async Task<Semester> GetActiveSemesterAsync()
        {
            return await _context.Semesters
                .FirstOrDefaultAsync(s => s.IsActive && !s.IsDeleted);
        }

        public async Task<Semester> GetSemesterByNameAsync(string semesterName)
        {
            return await _context.Semesters
                .FirstOrDefaultAsync(s => s.SemesterName == semesterName && !s.IsDeleted);
        }

        public async Task<IEnumerable<Semester>> GetUpcomingSemestersAsync()
        {
            return await _context.Semesters
                .Where(s => s.StartDate > DateTime.Now && !s.IsDeleted)
                .OrderBy(s => s.StartDate)
                .ToListAsync();
        }

        public async Task<bool> IsRegistrationOpenAsync(Guid semesterId)
        {
            var semester = await _context.Semesters
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == semesterId && !s.IsDeleted);

            if (semester == null)
                return false;

            return DateTime.Now <= semester.RegistrationDeadline;
        }

        public async Task<Semester> GetByIdAsync(Guid id)
        {
            return await _context.Semesters
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<IEnumerable<Semester>> GetAllAsync()
        {
            return await _context.Semesters
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.StartDate)
                .ToListAsync();
        }

        public async Task AddAsync(Semester entity)
        {
            await _context.Semesters.AddAsync(entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await _context.Semesters.FindAsync(id);
            if (entity != null)
                _context.Semesters.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
