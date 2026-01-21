using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly HupDbContext _context;

        public InstructorRepository(HupDbContext context)
        {
            _context = context;
        }

        public async Task<Instructor> GetByIdAsync(Guid id)
        {
            return await _context.Instructors
                .Include(i => i.User)
                .Include(i => i.Department)
                .Include(i => i.CourseOfferings)
                    .ThenInclude(coi => coi.CourseOffering)
                        .ThenInclude(co => co.Course)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _context.Instructors
                .Include(i => i.User)
                .Include(i => i.Department)
                .Where(i => !i.IsDeleted)
                .OrderBy(i => i.User.FullName)
                .ToListAsync();
        }

        public async Task AddAsync(Instructor entity)
        {
            await _context.Instructors.AddAsync(entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await _context.Instructors.FindAsync(id);
            if (entity != null)
                _context.Instructors.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Instructor>> GetByDepartmentAsync(Guid departmentId)
        {
            return await _context.Instructors
                .Include(i => i.User)
                .Include(i => i.Department)
                .Where(i => i.DepartmentId == departmentId && !i.IsDeleted)
                .OrderBy(i => i.User.FullName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Instructor>> GetByFacultyAsync(Guid facultyId)
        {
            return await _context.Instructors
                .Include(i => i.User)
                .Include(i => i.Department)
                .Where(i => i.Department.FacultyId == facultyId && !i.IsDeleted)
                .OrderBy(i => i.User.FullName)
                .ToListAsync();
        }

        public async Task<Instructor> GetByUserIdAsync(Guid userId)
        {
            return await _context.Instructors
                .Include(i => i.User)
                .Include(i => i.Department)
                    .ThenInclude(d => d.Faculty)
                .Include(i => i.CourseOfferings)
                    .ThenInclude(coi => coi.CourseOffering)
                        .ThenInclude(co => co.Course)
                .FirstOrDefaultAsync(i => i.UserId == userId && !i.IsDeleted);
        }

        public async Task<IEnumerable<Instructor>> GetAvailableInstructorsAsync(Guid courseId, Guid semesterId)
        {

            var instructors = await _context.Instructors
                .Include(i => i.User)
                .Include(i => i.Department)
                .Where(i => !i.IsDeleted)
                .ToListAsync();

            var availableInstructors = new List<Instructor>();

            foreach (var instructor in instructors)
            {
                availableInstructors.Add(instructor);
            }

            return availableInstructors;
        }

        public async Task<bool> IsInstructorAvailableAsync(Guid instructorId, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime)
        {
            var instructorGroups = await _context.ClassGroups
                .Where(cg => cg.InstructorId == instructorId
                          && cg.DayOfWeek == dayOfWeek
                          && cg.IsActive
                          && !cg.IsDeleted)
                .ToListAsync();

            foreach (var group in instructorGroups)
            {
                bool timeConflict = (startTime >= group.StartTime && startTime < group.EndTime) ||
                                    (endTime > group.StartTime && endTime <= group.EndTime) ||
                                    (startTime <= group.StartTime && endTime >= group.EndTime);

                if (timeConflict)
                    return false;
            }

            return true;
        }

        public async Task<IEnumerable<Course>> GetAssignedCoursesAsync(Guid instructorId, Guid semesterId)
        {
            return await _context.ClassGroups
                .Include(cg => cg.CourseOffering)
                    .ThenInclude(co => co.Course)
                .Where(cg => cg.InstructorId == instructorId
                          && cg.CourseOffering.SemesterId == semesterId
                          && cg.IsActive
                          && !cg.IsDeleted)
                .Select(cg => cg.CourseOffering.Course)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassGroup>> GetInstructorScheduleAsync(Guid instructorId, Guid semesterId)
        {
            return await _context.ClassGroups
                .Include(cg => cg.CourseOffering)
                    .ThenInclude(co => co.Course)
                .Include(cg => cg.Enrollments)
                .Where(cg => cg.InstructorId == instructorId
                          && cg.CourseOffering.SemesterId == semesterId
                          && cg.IsActive
                          && !cg.IsDeleted)
                .OrderBy(cg => cg.DayOfWeek)
                .ThenBy(cg => cg.StartTime)
                .ToListAsync();
        }
    }
}
