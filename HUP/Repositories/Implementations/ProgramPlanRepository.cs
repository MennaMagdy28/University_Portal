using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;

namespace HUP.Repositories.Implementations
{
    //ProgramPlan table has composite primary key (DepartmentId, CourseId) no single Id for entity both are foreign keys
    public class ProgramPlanRepository : IProgramPlanRepository
    {
        private readonly HupDbContext _context;
        public ProgramPlanRepository(HupDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProgramPlan entity)
        {
            await _context.ProgramPlan.AddAsync(entity);
        }

        public async Task<IEnumerable<ProgramPlan>> GetAllAsync()
        {
            return await _context.ProgramPlan.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ProgramPlan>> GetByDepartmentAsync(Guid departmentId)
        {
            var entities = await _context.ProgramPlan.Where(p => p.DepartmentId == departmentId)
                .AsNoTracking().ToListAsync();
            return entities;
        }

        public async Task<ProgramPlan> GetByIdAsync(Guid deptId, Guid courseId)
        {
            var p = await _context.ProgramPlan
                                .AsNoTracking()
                                .FirstOrDefaultAsync(pp => pp.DepartmentId == deptId 
                                && pp.CourseId == courseId);
            return p;
        }

        public async Task RemoveAsync(Guid deptId, Guid courseId)
        {
            var entity = await _context.ProgramPlan.FindAsync(deptId, courseId); // deptId and courseId need to be in this order
            if (entity != null)
            {
                _context.ProgramPlan.Remove(entity);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(ProgramPlan entity)
        {
            _context.ProgramPlan.Update(entity);
        }
    }
}
