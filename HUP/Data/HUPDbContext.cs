using HUP.Core.Entities.AcademicModels;
using HUP.Core.Entities.ServiceModels;
using HUP.Core.Entities.UserModels;
using Microsoft.EntityFrameworkCore;

namespace HUP.Data
{
    public class HUPDbContext : DbContext
    {
        public HUPDbContext(DbContextOptions<HUPDbContext> options) : base(options)
        {
        }

        // User Management
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePagePermission> RolePagePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserPagePermission> UserPagePermissions { get; set; }

        // Academic
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentPersonal> StudentPersonals { get; set; }
        public DbSet<StudentContacts> StudentContacts { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProgramEntity> Programs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Exam> Exams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.NationalID).IsUnique();
                entity.HasIndex(u => u.UniversityEmail).IsUnique();
                entity.Property(u => u.Role).HasConversion<string>();
            });

            // Student Configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(s => s.UniversityCode).IsUnique();
                entity.HasIndex(s => s.UniversityEmail).IsUnique();
                entity.Property(s => s.AcademicStatus).HasConversion<string>();
                entity.Property(s => s.CGPA).HasPrecision(3, 2);

                entity.HasOne(s => s.User)
                      .WithOne(u => u.Student)
                      .HasForeignKey<Student>(s => s.UserID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Faculty)
                      .WithMany(f => f.Students)
                      .HasForeignKey(s => s.FacultyID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Program)
                      .WithMany(p => p.Students)
                      .HasForeignKey(s => s.ProgramID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // StudentPersonal Configuration
            modelBuilder.Entity<StudentPersonal>(entity =>
            {
                entity.HasOne(sp => sp.Student)
                      .WithOne(s => s.StudentPersonal)
                      .HasForeignKey<StudentPersonal>(sp => sp.StudentID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // StudentContacts Configuration
            modelBuilder.Entity<StudentContacts>(entity =>
            {
                entity.HasOne(sc => sc.Student)
                      .WithOne(s => s.StudentContacts)
                      .HasForeignKey<StudentContacts>(sc => sc.StudentID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Enrollment Configuration
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasIndex(e => new { e.StudentID, e.CourseID, e.Semester }).IsUnique();
                entity.Property(e => e.Status).HasConversion<string>();

                entity.HasOne(e => e.Student)
                      .WithMany(s => s.Enrollments)
                      .HasForeignKey(e => e.StudentID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Course)
                      .WithMany(c => c.Enrollments)
                      .HasForeignKey(e => e.CourseID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Exam Configuration
            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.ExamType).HasConversion<string>();

                entity.HasOne(e => e.Course)
                      .WithMany(c => c.Exams)
                      .HasForeignKey(e => e.CourseID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Department Configuration
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasOne(d => d.Faculty)
                      .WithMany(f => f.Departments)
                      .HasForeignKey(d => d.FacultyID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Program Configuration
            modelBuilder.Entity<ProgramEntity>(entity =>
            {
                entity.HasOne(p => p.Department)
                      .WithMany(d => d.Programs)
                      .HasForeignKey(p => p.DepartmentID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Course Configuration
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasOne(c => c.Department)
                      .WithMany(d => d.Courses)
                      .HasForeignKey(c => c.DepartmentID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Prerequisite)
                      .WithMany(c => c.PrerequisitesFor)
                      .HasForeignKey(c => c.PrerequisiteID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Instructor Configuration
            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.HasOne(i => i.User)
                      .WithOne(u => u.Instructor)
                      .HasForeignKey<Instructor>(i => i.UserID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.Department)
                      .WithMany(d => d.Instructors)
                      .HasForeignKey(i => i.DepartmentID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Many-to-Many Relationships
            modelBuilder.Entity<RolePagePermission>(entity =>
            {
                entity.HasKey(rpp => new { rpp.RoleID, rpp.PageID, rpp.PermissionID });

                entity.HasOne(rpp => rpp.Role)
                      .WithMany(r => r.RolePagePermissions)
                      .HasForeignKey(rpp => rpp.RoleID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(rpp => rpp.Page)
                      .WithMany(p => p.RolePagePermissions)
                      .HasForeignKey(rpp => rpp.PageID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(rpp => rpp.Permission)
                      .WithMany(p => p.RolePagePermissions)
                      .HasForeignKey(rpp => rpp.PermissionID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserPagePermission>(entity =>
            {
                entity.HasKey(upp => new { upp.UserID, upp.PageID, upp.PermissionID });

                entity.HasOne(upp => upp.User)
                      .WithMany(u => u.UserPagePermissions)
                      .HasForeignKey(upp => upp.UserID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(upp => upp.Page)
                      .WithMany(p => p.UserPagePermissions)
                      .HasForeignKey(upp => upp.PageID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(upp => upp.Permission)
                      .WithMany(p => p.UserPagePermissions)
                      .HasForeignKey(upp => upp.PermissionID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleID)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
