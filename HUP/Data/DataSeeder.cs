using HUP.Core.Enums;
using HUP.Core.Models.AcademicModels;
using HUP.Core.Models.UserModels;

namespace HUP.Data
{
    public static class DataSeeder
    {
        public static void Seed(HUPDbContext context)
        {
            // Seed Roles
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
            {
                new Role { RoleName = "SuperAdmin", RoleDescription = "System Super Administrator", CreatedBy = 1 },
                new Role { RoleName = "Admin", RoleDescription = "University Administrator", CreatedBy = 1 },
                new Role { RoleName = "Registrar", RoleDescription = "Registration Department", CreatedBy = 1 },
                new Role { RoleName = "Instructor", RoleDescription = "Teaching Staff", CreatedBy = 1 },
                new Role { RoleName = "Student", RoleDescription = "University Student", CreatedBy = 1 }
            };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            // Seed Super Admin User
            if (!context.Users.Any(u => u.Role == RoleType.SuperAdmin))
            {
                var superAdmin = new User
                {
                    NationalID = "0000000000",
                    UniversityEmail = "superadmin@university.edu",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    FullName = "System Super Administrator",
                    Phone = "+966500000000",
                    Role = RoleType.SuperAdmin,
                    IsActive = true
                };
                context.Users.Add(superAdmin);
                context.SaveChanges();

                // Assign SuperAdmin role
                var superAdminRole = context.Roles.First(r => r.RoleName == "SuperAdmin");
                var userRole = new UserRole
                {
                    UserID = superAdmin.Id,
                    RoleID = superAdminRole.Id,
                    AssignedBy = superAdmin.Id,
                    ExpiryDate = DateOnly.FromDateTime(DateTime.Now.AddYears(10))
                };
                context.UserRoles.Add(userRole);
                context.SaveChanges();
            }

            // Seed Sample Faculty
            if (!context.Faculties.Any())
            {
                var faculty = new Faculty
                {
                    FacultyName = "كلية علوم الحاسب والمعلومات",
                    DeanName = "د. أحمد محمد",
                    ContactInfo = "ccis@university.edu"
                };
                context.Faculties.Add(faculty);
                context.SaveChanges();

                // Seed Sample Department
                var department = new Department
                {
                    FacultyID = faculty.Id,
                    DepartmentName = "قسم علوم الحاسب",
                    HeadOfDepartment = "د. سعيد عبدالله"
                };
                context.Departments.Add(department);
                context.SaveChanges();

                // Seed Sample Program
                var program = new ProgramEntity
                {
                    DepartmentID = department.Id,
                    ProgramName = "بكالوريوس علوم الحاسب",
                    DegreeType = "Bachelor",
                    DurationYears = 4,
                    Credits = 135
                };
                context.Programs.Add(program);
                context.SaveChanges();
            }
        }
    }
}
