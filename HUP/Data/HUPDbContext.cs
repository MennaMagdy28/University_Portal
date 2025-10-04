using HUP.Core.Enums;
using HUP.Core.Models.AcademicModels;
using HUP.Core.Models.ServiceModels;
using HUP.Core.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace HUP.Data
{
    public class HUPDbContext : DbContext
    {
        public HUPDbContext(DbContextOptions<HUPDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePagePermission> RolePagePermissions { get; set; }
        public DbSet<UserPagePermission> UserPagePermissions { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentPersonal> StudentPersonals { get; set; }
        public DbSet<StudentContacts> StudentContacts { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProgramEntity> Programs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RolePagePermission>()
                .HasKey(rpp => new { rpp.RoleId, rpp.PageId, rpp.PermissionId });

            modelBuilder.Entity<RolePagePermission>()
                .HasOne(rpp => rpp.Role)
                .WithMany(r => r.RolePagePermissions)
                .HasForeignKey(rpp => rpp.RoleId);

            modelBuilder.Entity<RolePagePermission>()
                .HasOne(rpp => rpp.Page)
                .WithMany(p => p.RolePagePermissions)
                .HasForeignKey(rpp => rpp.PageId);

            modelBuilder.Entity<RolePagePermission>()
                .HasOne(rpp => rpp.Permission)
                .WithMany()
                .HasForeignKey(rpp => rpp.PermissionId);

            modelBuilder.Entity<Student>()
                .Property(s => s.AcademicStatus)
                .HasConversion<string>();

            modelBuilder.Entity<StudentPersonal>()
                .Property(sp => sp.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<ProgramEntity>()
                .Property(p => p.DegreeType)
                .HasConversion<string>();

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Exam>()
                .Property(e => e.ExamType)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.NationalID)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.UniversityCode)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.UniversityEmail)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Student>()
                .Property(s => s.AcademicStatus)
                .HasDefaultValue(AcademicStatus.Active);

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Status)
                .HasDefaultValue(EnrollmentStatus.Registered);
        }
    }
}
