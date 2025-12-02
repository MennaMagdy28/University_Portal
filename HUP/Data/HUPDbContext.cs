using Microsoft.EntityFrameworkCore;
using HUP.Core.Entities.Academics;
using HUP.Core.Entities.Identity;
using HUP.Core.Entities.Permissions;


namespace HUP.Data
{
    public class HupDbContext : DbContext
    {
        public HupDbContext(DbContextOptions<HupDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<CourseOffering> CourseOfferings { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ProgramPlan> ProgramPlan { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<CourseOfferingInstructor> CourseOfferingInstructors { get; set; }

        public DbSet<CourseSchedule> CourseSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // RolePermission (Many-to-Many)
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // User ↔ Role (Many-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // User ↔ UserContact (One-to-One)
            // User ↔ UserPersonalInfo (One-to-One)
            modelBuilder.Entity<User>()
                .OwnsOne<UserPersonalInfo>(u => u.PersonalInfo);
            modelBuilder.Entity<User>()
                .OwnsOne<UserContact>(u => u.ContactInfo);

            // User ↔ Student (One-to-One)
            modelBuilder.Entity<Student>()
                .HasKey(s => s.UserId);
            modelBuilder.Entity<Student>()
                .HasOne( s => s.User)
                .WithOne()
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ Instructor (One-to-One)
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.User)
                .WithOne()
                .HasForeignKey<Instructor>(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ Faculty (One-to-One - Dean)
            modelBuilder.Entity<Faculty>()
                .HasOne(f => f.Dean)
                .WithOne()
                .HasForeignKey<Faculty>(f => f.DeanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Role ↔ User (CreatedBy)
            modelBuilder.Entity<Role>()
                .HasOne(r => r.CreatedByUser)
                .WithMany(u => u.CreatedRoles)
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Faculty ↔ Department (One-to-Many)
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Faculty)
                .WithMany(f => f.Departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department ↔ Instructor (One-to-Many)
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department ↔ Instructor (One-to-One)
            modelBuilder.Entity<Department>()
                .HasOne(d => d.HeadOfDepartment)
                .WithOne(i => i.DepartmentHeaded)
                .HasForeignKey<Department>(d => d.HeadOfDepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
            
            
            modelBuilder.Entity<ProgramPlan>()
                .HasKey(pp => new { pp.DepartmentId, pp.CourseId });

            // Department ↔ ProgramPlan (One-to-Many)
            modelBuilder.Entity<ProgramPlan>()
                .HasOne(p => p.Department)
                .WithMany(d => d.Programs)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course ↔ ProgramPlan (One-to-Many)
            modelBuilder.Entity<ProgramPlan>()
                .HasOne(p => p.Course)
                .WithMany(c=>c.Programs)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course ↔ Course (Self-referential Prerequisite)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Prerequisite)
                .WithMany()
                .HasForeignKey(c => c.PrerequisiteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course ↔ CourseOffering (One-to-Many)
            modelBuilder.Entity<CourseOffering>()
                .HasOne(co => co.Course)
                .WithMany(c => c.CourseOfferings)
                .HasForeignKey(co => co.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Instructor ↔ CourseOffering (Many-to-Many)
            modelBuilder.Entity<CourseOfferingInstructor>()
                .HasKey(coi => new { coi.CourseOfferingId, coi.InstructorId });
            
            modelBuilder.Entity<CourseOfferingInstructor>()
                .HasOne(coi => coi.CourseOffering)
                .WithMany(coi => coi.Instructors)
                .HasForeignKey(coi => coi.CourseOfferingId);
            
            modelBuilder.Entity<CourseOfferingInstructor>()
                .HasOne(coi => coi.Instructor)
                .WithMany(coi => coi.CourseOfferings)
                .HasForeignKey(coi => coi.InstructorId);
            
            // Semester ↔ CourseOffering (One-to-Many)
            modelBuilder.Entity<CourseOffering>()
                .HasOne(co => co.Semester)
                .WithMany()
                .HasForeignKey(co => co.SemesterId)
                .OnDelete(DeleteBehavior.Restrict);

            // CourseOffering ↔ Department (Many-to-One)
            modelBuilder.Entity<CourseOffering>()
                .HasOne(co => co.Department)
                .WithMany(d => d.CourseOfferings)
                .HasForeignKey(co => co.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student ↔ Department (Many-to-One)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany()
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student ↔ Enrollment (One-to-Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // CourseOffering ↔ Enrollment (One-to-Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.CourseOffering)
                .WithMany(co => co.Enrollments)
                .HasForeignKey(e => e.CourseOfferingId)
                .OnDelete(DeleteBehavior.Restrict);

            // CourseOffering ↔ Exam (One-to-Many)
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.CourseOffering)
                .WithMany(co => co.Exams)
                .HasForeignKey(e => e.CourseOfferingId)
                .OnDelete(DeleteBehavior.Restrict);

            // CourseOffering ↔ Schedule (One-to-Many)
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.CourseOffering)
                .WithMany(co => co.Schedules)
                .HasForeignKey(s => s.CourseOfferingId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
