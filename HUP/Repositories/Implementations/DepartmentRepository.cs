using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HUPDbContext _context;
        public DepartmentRepository(HUPDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Department entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _context.Departments.AddAsync(entity);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<Department> GetByIdAsync(Guid id)
        {
            var dept = await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
            return dept;
        }

        public void Remove(Guid id)
        {
            if (id == Guid.Empty) return;
            var entity = _context.Departments.Find(id);
            if (entity == null) return;
            _context.Departments.Remove(entity);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void SoftDelete(Guid id)
        {
            if (id == Guid.Empty) return;
            var entity = _context.Departments.Find(id);
            if (entity == null) return;
            entity.IsDeleted = true;
            _context.Departments.Update(entity);
        }

        public void Update(Department entity)
        {
            _context.Departments.Update(entity);
        }
    }
}
