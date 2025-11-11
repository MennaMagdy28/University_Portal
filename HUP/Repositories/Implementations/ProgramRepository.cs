using HUP.Core.Entities.AcademicModels;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly HUPDbContext _context;

        public ProgramRepository(HUPDbContext context)
        {
            _context = context;
        }

        public async Task<ProgramEntity> GetByIdAsync(int id)
        {
            return await _context.Programs
                .Include(p => p.Department)
                .ThenInclude(d => d.Faculty)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProgramEntity>> GetAllAsync()
        {
            return await _context.Programs
                .Include(p => p.Department)
                .ThenInclude(d => d.Faculty)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProgramEntity>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _context.Programs
                .Include(p => p.Department)
                .Where(p => p.DepartmentID == departmentId && p.IsActive)
                .ToListAsync();
        }

        public async Task AddAsync(ProgramEntity program)
        {
            await _context.Programs.AddAsync(program);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProgramEntity program)
        {
            program.UpdatedAt = DateTime.UtcNow;
            _context.Programs.Update(program);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var program = await GetByIdAsync(id);
            if (program != null)
            {
                program.IsActive = false;
                program.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(program);
            }
        }
    }
}
