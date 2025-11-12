using Microsoft.EntityFrameworkCore;
using HUP.Core.Entities.Academics;
using HUP.Core.Entities.Identity;
using HUP.Core.Entities.Permissions;


namespace HUP.Data
{
    public class HUPDbContext : DbContext
    {
        public HUPDbContext(DbContextOptions<HUPDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
        public DbSet<UserPersonalInfo> UserPersonalInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<CourseOffering> courseOfferings { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ProgramEntity> Programs { get; set; }
        public DbSet<Semester> Semesters { get; set; }

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
            modelBuilder.Entity<UserContact>()
                .HasKey(uc => uc.UserID);
            modelBuilder.Entity<UserContact>()
                .HasOne(uc => uc.User)
                .WithOne()
                .HasForeignKey<UserContact>(uc => uc.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ UserPersonalInfo (One-to-One)
            modelBuilder.Entity<UserPersonalInfo>()
                .HasKey(upi => upi.UserID);
            modelBuilder.Entity<UserPersonalInfo>()
                .HasOne(upi => upi.User)
                .WithOne()
                .HasForeignKey<UserPersonalInfo>(upi => upi.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ Student (One-to-One)
            modelBuilder.Entity<Student>()
                .HasKey(s => s.UserID);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Student>(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ Instructor (One-to-One)
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.User)
                .WithOne()
                .HasForeignKey<Instructor>(i => i.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ Faculty (One-to-One - Dean)
            modelBuilder.Entity<Faculty>()
                .HasOne(f => f.Dean)
                .WithOne()
                .HasForeignKey<Faculty>(f => f.DeanID)
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
                .HasForeignKey(d => d.FacultyID)
                .OnDelete(DeleteBehavior.Restrict);

            // Department ↔ Course (One-to-Many)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Department ↔ Instructor (One-to-Many)
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Department ↔ Program (One-to-Many)
            modelBuilder.Entity<ProgramEntity>()
                .HasOne(p => p.Department)
                .WithMany()
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course ↔ Course (Self-referential Prerequisite)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Prerequisite)
                .WithMany()
                .HasForeignKey(c => c.PrerequisiteID)
                .OnDelete(DeleteBehavior.Restrict);

            // Course ↔ CourseOffering (One-to-Many)
            modelBuilder.Entity<CourseOffering>()
                .HasOne(co => co.Course)
                .WithMany()
                .HasForeignKey(co => co.CourseID)
                .OnDelete(DeleteBehavior.Restrict);

            // Instructor ↔ CourseOffering (One-to-Many)
            modelBuilder.Entity<CourseOffering>()
                .HasOne(co => co.Instructor)
                .WithMany()
                .HasForeignKey(co => co.InstructorID)
                .OnDelete(DeleteBehavior.Restrict);

            // Semester ↔ CourseOffering (One-to-Many)
            modelBuilder.Entity<CourseOffering>()
                .HasOne(co => co.Semester)
                .WithMany()
                .HasForeignKey(co => co.SemesterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student ↔ Program (Many-to-One)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Program)
                .WithMany()
                .HasForeignKey(s => s.ProgramID)
                .OnDelete(DeleteBehavior.Restrict);

            // Student ↔ Enrollment (One-to-Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            // CourseOffering ↔ Enrollment (One-to-Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.CourseOffering)
                .WithMany()
                .HasForeignKey(e => e.CourseOfferingId)
                .OnDelete(DeleteBehavior.Restrict);

            // CourseOffering ↔ Exam (One-to-Many)
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.CourseOffering)
                .WithMany()
                .HasForeignKey(e => e.CousreOfferingId)
                .OnDelete(DeleteBehavior.Restrict);

            // CourseOffering ↔ Schedule (One-to-Many)
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.CourseOffering)
                .WithMany()
                .HasForeignKey(s => s.CourseOfferingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
