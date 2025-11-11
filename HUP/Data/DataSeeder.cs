using HUP.Core.Enums;
using HUP.Core.Entities.AcademicModels;
using HUP.Core.Entities.ServiceModels;
using HUP.Core.Entities.UserModels;

namespace HUP.Data
{
    public static class DataSeeder
    {
        public static void Seed(HUPDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            ClearExistingData(context);

            SeedRoles(context);
            SeedSuperAdmin(context);
            SeedAdminAndRegistrar(context);
            SeedPagesAndPermissions(context);
            SeedFacultiesAndDepartments(context);
            SeedPrograms(context);
            SeedCourses(context);
            SeedInstructors(context);
            SeedStudents(context);
            SeedEnrollments(context);
            SeedExams(context);
            SeedRolePermissions(context);

            context.SaveChanges();
        }

        private static void SeedRoles(HUPDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
            {
                new Role {
                    RoleName = "SuperAdmin",
                    RoleDescription = "System Super Administrator",
                    CreatedBy = 1,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Role {
                    RoleName = "Admin",
                    RoleDescription = "University Administrator",
                    CreatedBy = 1,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Role {
                    RoleName = "Registrar",
                    RoleDescription = "Registration Department",
                    CreatedBy = 1,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Role {
                    RoleName = "Instructor",
                    RoleDescription = "Teaching Staff",
                    CreatedBy = 1,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Role {
                    RoleName = "Student",
                    RoleDescription = "University Student",
                    CreatedBy = 1,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }
        }

        private static void SeedSuperAdmin(HUPDbContext context)
        {
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
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
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
                    ExpiryDate = DateOnly.FromDateTime(DateTime.Now.AddYears(10)),
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                context.UserRoles.Add(userRole);
                context.SaveChanges();
            }
        }

        private static void SeedPagesAndPermissions(HUPDbContext context)
        {
            if (!context.Pages.Any())
            {
                var pages = new List<Page>
            {
                new Page { PageName = "Dashboard", PageDescription = "Main Dashboard", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Page { PageName = "UserManagement", PageDescription = "User Management", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Page { PageName = "StudentManagement", PageDescription = "Student Management", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Page { PageName = "FacultyManagement", PageDescription = "Faculty Management", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Page { PageName = "CourseManagement", PageDescription = "Course Management", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Page { PageName = "Enrollment", PageDescription = "Course Enrollment", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Page { PageName = "Grades", PageDescription = "Grades Management", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Page { PageName = "Reports", PageDescription = "Reports and Analytics", CreatedAt = DateTime.UtcNow, IsActive = true }
            };
                context.Pages.AddRange(pages);
                context.SaveChanges();
            }

            if (!context.Permissions.Any())
            {
                var permissions = new List<Permission>
            {
                new Permission { PermissionName = "View", PermissionDescription = "View Permission", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Permission { PermissionName = "Create", PermissionDescription = "Create Permission", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Permission { PermissionName = "Edit", PermissionDescription = "Edit Permission", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Permission { PermissionName = "Delete", PermissionDescription = "Delete Permission", CreatedAt = DateTime.UtcNow, IsActive = true },
                new Permission { PermissionName = "Approve", PermissionDescription = "Approve Permission", CreatedAt = DateTime.UtcNow, IsActive = true }
            };
                context.Permissions.AddRange(permissions);
                context.SaveChanges();
            }
        }

        private static void SeedFacultiesAndDepartments(HUPDbContext context)
        {
            if (!context.Faculties.Any())
            {
                var faculties = new List<Faculty>
            {
                new Faculty
                {
                    FacultyName = "كلية علوم الحاسب والمعلومات",
                    DeanName = "د. أحمد محمد",
                    ContactInfo = "ccis@university.edu - 0112345678",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Faculty
                {
                    FacultyName = "كلية الهندسة",
                    DeanName = "د. خالد عبدالله",
                    ContactInfo = "engineering@university.edu - 0112345679",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Faculty
                {
                    FacultyName = "كلية إدارة الأعمال",
                    DeanName = "د. سارة أحمد",
                    ContactInfo = "business@university.edu - 0112345680",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.Faculties.AddRange(faculties);
                context.SaveChanges();

                // Seed Departments
                var ccisFaculty = context.Faculties.First(f => f.FacultyName.Contains("الحاسب"));
                var engineeringFaculty = context.Faculties.First(f => f.FacultyName.Contains("الهندسة"));
                var businessFaculty = context.Faculties.First(f => f.FacultyName.Contains("الأعمال"));

                var departments = new List<Department>
            {
                new Department
                {
                    FacultyID = ccisFaculty.Id,
                    DepartmentName = "قسم علوم الحاسب",
                    HeadOfDepartment = "د. سعيد عبدالله",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Department
                {
                    FacultyID = ccisFaculty.Id,
                    DepartmentName = "قسم نظم المعلومات",
                    HeadOfDepartment = "د. فاطمة علي",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Department
                {
                    FacultyID = engineeringFaculty.Id,
                    DepartmentName = "قسم الهندسة الكهربائية",
                    HeadOfDepartment = "د. محمد حسن",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Department
                {
                    FacultyID = businessFaculty.Id,
                    DepartmentName = "قسم إدارة الأعمال",
                    HeadOfDepartment = "د. نورة الكبير",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.Departments.AddRange(departments);
                context.SaveChanges();
            }
        }

        private static void SeedPrograms(HUPDbContext context)
        {
            if (!context.Programs.Any())
            {
                var csDept = context.Departments.First(d => d.DepartmentName.Contains("علوم الحاسب"));
                var isDept = context.Departments.First(d => d.DepartmentName.Contains("نظم المعلومات"));
                var eeDept = context.Departments.First(d => d.DepartmentName.Contains("الكهربائية"));
                var businessDept = context.Departments.First(d => d.DepartmentName.Contains("إدارة الأعمال"));

                var programs = new List<ProgramEntity>
            {
                new ProgramEntity
                {
                    DepartmentID = csDept.Id,
                    ProgramName = "بكالوريوس علوم الحاسب",
                    DegreeType = "Bachelor",
                    DurationYears = 4,
                    Credits = 135,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new ProgramEntity
                {
                    DepartmentID = isDept.Id,
                    ProgramName = "بكالوريوس نظم المعلومات",
                    DegreeType = "Bachelor",
                    DurationYears = 4,
                    Credits = 132,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new ProgramEntity
                {
                    DepartmentID = eeDept.Id,
                    ProgramName = "بكالوريوس الهندسة الكهربائية",
                    DegreeType = "Bachelor",
                    DurationYears = 5,
                    Credits = 165,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new ProgramEntity
                {
                    DepartmentID = businessDept.Id,
                    ProgramName = "بكالوريوس إدارة الأعمال",
                    DegreeType = "Bachelor",
                    DurationYears = 4,
                    Credits = 120,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.Programs.AddRange(programs);
                context.SaveChanges();
            }
        }

        private static void SeedCourses(HUPDbContext context)
        {
            if (!context.Courses.Any())
            {
                var csDept = context.Departments.FirstOrDefault(d => d.DepartmentName.Contains("علوم الحاسب"));
                var isDept = context.Departments.FirstOrDefault(d => d.DepartmentName.Contains("نظم المعلومات"));

                var courses = new List<Course>
            {
                new Course
                {
                    CourseCode = "CS101",
                    CourseName = "مقدمة في البرمجة",
                    Credits = 3,
                    DepartmentID = csDept.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Course
                {
                    CourseCode = "CS201",
                    CourseName = "هياكل البيانات",
                    Credits = 3,
                    DepartmentID = csDept.Id,
                    PrerequisiteID = null,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Course
                {
                    CourseCode = "CS301",
                    CourseName = "قواعد البيانات",
                    Credits = 3,
                    DepartmentID = csDept.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Course
                {
                    CourseCode = "IS101",
                    CourseName = "مقدمة في نظم المعلومات",
                    Credits = 3,
                    DepartmentID = isDept.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Course
                {
                    CourseCode = "MATH101",
                    CourseName = "رياضيات عامة",
                    Credits = 4,
                    DepartmentID = csDept.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.Courses.AddRange(courses);
                context.SaveChanges();

                // Set prerequisites after courses are created
                var dataStructures = context.Courses.First(c => c.CourseCode == "CS201");
                var introToProgramming = context.Courses.First(c => c.CourseCode == "CS101");
                dataStructures.PrerequisiteID = introToProgramming.Id;
                context.SaveChanges();
            }
        }

        private static void SeedInstructors(HUPDbContext context)
        {
            if (!context.Instructors.Any())
            {
                var csDept = context.Departments.FirstOrDefault(d => d.DepartmentName.Contains("علوم الحاسب"));
                var isDept = context.Departments.FirstOrDefault(d => d.DepartmentName.Contains("نظم المعلومات"));

                // Create instructor users first
                var instructorUsers = new List<User>
            {
                new User
                {
                    NationalID = "1111111111",
                    UniversityEmail = "dr.ali@university.edu",
                    Password = BCrypt.Net.BCrypt.HashPassword("1111111111"),
                    FullName = "د. علي محمد",
                    Phone = "+966501111111",
                    Role = RoleType.Instructor,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    NationalID = "1111111112",
                    UniversityEmail = "dr.fatima@university.edu",
                    Password = BCrypt.Net.BCrypt.HashPassword("1111111112"),
                    FullName = "د. فاطمة أحمد",
                    Phone = "+966501111112",
                    Role = RoleType.Instructor,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
                context.Users.AddRange(instructorUsers);
                context.SaveChanges();

                var instructors = new List<Instructor>
            {
                new Instructor
                {
                    UserID = instructorUsers[0].Id,
                    DepartmentID = csDept.Id,
                    AcademicTitle = "أستاذ مساعد",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Instructor
                {
                    UserID = instructorUsers[1].Id,
                    DepartmentID = isDept.Id,
                    AcademicTitle = "أستاذ مشارك",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.Instructors.AddRange(instructors);
                context.SaveChanges();
            }
        }

        private static void SeedStudents(HUPDbContext context)
        {
            if (!context.Students.Any())
            {
                var csProgram = context.Programs.FirstOrDefault(p => p.ProgramName.Contains("علوم الحاسب"));
                var isProgram = context.Programs.FirstOrDefault(p => p.ProgramName.Contains("نظم المعلومات"));
                var csFaculty = context.Faculties.FirstOrDefault(f => f.FacultyName.Contains("الحاسب"));

                var studentUsers = new List<User>
            {
                new User
                {
                    NationalID = "1000000001",
                    UniversityEmail = "student1@personal.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("1000000001"),
                    FullName = "محمد عبدالرحمن",
                    Phone = "+966501234567",
                    Role = RoleType.Student,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    NationalID = "1000000002",
                    UniversityEmail = "student2@personal.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("1000000002"),
                    FullName = "أحمد خالد",
                    Phone = "+966501234568",
                    Role = RoleType.Student,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    NationalID = "1000000003",
                    UniversityEmail = "student3@personal.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("1000000003"),
                    FullName = "سارة محمد",
                    Phone = "+966501234569",
                    Role = RoleType.Student,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
                context.Users.AddRange(studentUsers);
                context.SaveChanges();

                var students = new List<Student>
            {
                new Student
                {
                    UserID = studentUsers[0].Id,
                    UniversityCode = "CS2024001",
                    UniversityEmail = "CS2024001@university.edu",
                    ProfileImage = null,
                    AcademicStatus = AcademicStatus.Active,
                    FacultyID = csFaculty.Id,
                    ProgramID = csProgram.Id,
                    Level = 2,
                    CGPA = 3.75m,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Student
                {
                    UserID = studentUsers[1].Id,
                    UniversityCode = "CS2024002",
                    UniversityEmail = "CS2024002@university.edu",
                    ProfileImage = null,
                    AcademicStatus = AcademicStatus.Active,
                    FacultyID = csFaculty.Id,
                    ProgramID = csProgram.Id,
                    Level = 1,
                    CGPA = 3.20m,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Student
                {
                    UserID = studentUsers[2].Id,
                    UniversityCode = "IS2024001",
                    UniversityEmail = "IS2024001@university.edu",
                    ProfileImage = null,
                    AcademicStatus = AcademicStatus.Active,
                    FacultyID = csFaculty.Id,
                    ProgramID = isProgram.Id,
                    Level = 3,
                    CGPA = 4.00m,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.Students.AddRange(students);
                context.SaveChanges();

                // Seed student personal information
                var studentPersonal = new List<StudentPersonal>
            {
                new StudentPersonal
                {
                    StudentID = students[0].Id,
                    Gender = Gender.Male,
                    BirthDate = new DateOnly(2000, 5, 15),
                    Religion = "Islam",
                    Nationality = "Saudi",
                    BirthPlace = "الرياض",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new StudentPersonal
                {
                    StudentID = students[1].Id,
                    Gender = Gender.Male,
                    BirthDate = new DateOnly(2001, 8, 22),
                    Religion = "Islam",
                    Nationality = "Saudi",
                    BirthPlace = "جدة",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new StudentPersonal
                {
                    StudentID = students[2].Id,
                    Gender = Gender.Female,
                    BirthDate = new DateOnly(1999, 12, 10),
                    Religion = "Islam",
                    Nationality = "Saudi",
                    BirthPlace = "الدمام",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.StudentPersonals.AddRange(studentPersonal);
                context.SaveChanges();

                // Seed student contacts
                var studentContacts = new List<StudentContacts>
            {
                new StudentContacts
                {
                    StudentID = students[0].Id,
                    Address = "شارع الملك فهد، الرياض",
                    City = "الرياض",
                    PhoneNumber = "+966501234567",
                    AltEmail = "mohammed@personal.com",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new StudentContacts
                {
                    StudentID = students[1].Id,
                    Address = "حي السلامة، جدة",
                    City = "جدة",
                    PhoneNumber = "+966501234568",
                    AltEmail = "ahmed@personal.com",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new StudentContacts
                {
                    StudentID = students[2].Id,
                    Address = "حي النخيل، الدمام",
                    City = "الدمام",
                    PhoneNumber = "+966501234569",
                    AltEmail = "sara@personal.com",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.StudentContacts.AddRange(studentContacts);
                context.SaveChanges();
            }
        }

        private static void SeedEnrollments(HUPDbContext context)
        {
            if (!context.Enrollments.Any())
            {
                var students = context.Students.Take(3).ToList();
                var courses = context.Courses.Take(3).ToList();

                var enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    StudentID = students[0].Id,
                    CourseID = courses[0].Id,
                    Semester = "Fall 2024",
                    Status = EnrollmentStatus.InProgress,
                    Grade = "",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Enrollment
                {
                    StudentID = students[0].Id,
                    CourseID = courses[1].Id,
                    Semester = "Fall 2024",
                    Status = EnrollmentStatus.InProgress,
                    Grade = "",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Enrollment
                {
                    StudentID = students[1].Id,
                    CourseID = courses[0].Id,
                    Semester = "Fall 2024",
                    Status = EnrollmentStatus.InProgress,
                    Grade = "",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Enrollment
                {
                    StudentID = students[2].Id,
                    CourseID = courses[2].Id,
                    Semester = "Fall 2024",
                    Status = EnrollmentStatus.Completed,
                    Grade = "A+",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.Enrollments.AddRange(enrollments);
                context.SaveChanges();
            }
        }

        private static void SeedExams(HUPDbContext context)
        {
            if (!context.Exams.Any())
            {
                var courses = context.Courses.Take(2).ToList();

                var exams = new List<Exam>
            {
                new Exam
                {
                    CourseID = courses[0].Id,
                    ExamType = ExamType.Midterm,
                    ExamDate = new DateOnly(2024, 10, 15),
                    ExamTime = new TimeOnly(9, 0, 0),
                    Location = "قاعة 101 - مبنى الكلية",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Exam
                {
                    CourseID = courses[0].Id,
                    ExamType = ExamType.Final,
                    ExamDate = new DateOnly(2024, 12, 20),
                    ExamTime = new TimeOnly(9, 0, 0),
                    Location = "قاعة 101 - مبنى الكلية",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Exam
                {
                    CourseID = courses[1].Id,
                    ExamType = ExamType.Midterm,
                    ExamDate = new DateOnly(2024, 10, 17),
                    ExamTime = new TimeOnly(11, 0, 0),
                    Location = "قاعة 203 - مبنى الكلية",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };
                context.Exams.AddRange(exams);
                context.SaveChanges();
            }
        }

        private static void SeedRolePermissions(HUPDbContext context)
        {
            if (!context.RolePagePermissions.Any())
            {
                var superAdminRole = context.Roles.First(r => r.RoleName == "SuperAdmin");
                var adminRole = context.Roles.First(r => r.RoleName == "Admin");
                var pages = context.Pages.ToList();
                var permissions = context.Permissions.ToList();

                var rolePermissions = new List<RolePagePermission>();

                // SuperAdmin gets all permissions on all pages
                foreach (var page in pages)
                {
                    foreach (var permission in permissions)
                    {
                        rolePermissions.Add(new RolePagePermission
                        {
                            RoleID = superAdminRole.Id,
                            PageID = page.Id,
                            PermissionID = permission.Id,
                            CreatedAt = DateTime.UtcNow,
                            IsActive = true
                        });
                    }
                }

                // Admin gets most permissions but not all
                var adminPages = pages.Where(p => !p.PageName.Contains("UserManagement")).ToList();
                var adminPermissions = permissions.Where(p => !p.PermissionName.Contains("Delete")).ToList();

                foreach (var page in adminPages)
                {
                    foreach (var permission in adminPermissions)
                    {
                        rolePermissions.Add(new RolePagePermission
                        {
                            RoleID = adminRole.Id,
                            PageID = page.Id,
                            PermissionID = permission.Id,
                            CreatedAt = DateTime.UtcNow,
                            IsActive = true
                        });
                    }
                }

                context.RolePagePermissions.AddRange(rolePermissions);
                context.SaveChanges();
            }
        }

        private static void SeedAdminAndRegistrar(HUPDbContext context)
        {
            if (!context.Users.Any(u => u.Role == RoleType.Admin))
            {
                // التحقق من وجود دور Admin أولاً
                var adminRole = context.Roles.FirstOrDefault(r => r.RoleName == "Admin");
                if (adminRole == null)
                {
                    // إذا لم يوجد، قم بإنشائه
                    adminRole = new Role
                    {
                        RoleName = "Admin",
                        RoleDescription = "University Administrator",
                        CreatedBy = 1,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    context.Roles.Add(adminRole);
                    context.SaveChanges();
                }

                var adminUser = new User
                {
                    NationalID = "2222222221",
                    UniversityEmail = "admin@university.edu",
                    Password = BCrypt.Net.BCrypt.HashPassword("2222222221"),
                    FullName = "مدير النظام",
                    Phone = "+966502222221",
                    Role = RoleType.Admin,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                context.Users.Add(adminUser);
                context.SaveChanges();

                var userRole = new UserRole
                {
                    UserID = adminUser.Id,
                    RoleID = adminRole.Id,
                    AssignedBy = 1, // SuperAdmin
                    ExpiryDate = DateOnly.FromDateTime(DateTime.Now.AddYears(2)),
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                context.UserRoles.Add(userRole);
                context.SaveChanges();
            }

            if (!context.Users.Any(u => u.Role == RoleType.Registrar))
            {
                // نفس الشيء لدور Registrar
                var registrarRole = context.Roles.FirstOrDefault(r => r.RoleName == "Registrar");
                if (registrarRole == null)
                {
                    registrarRole = new Role
                    {
                        RoleName = "Registrar",
                        RoleDescription = "Registration Department",
                        CreatedBy = 1,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    context.Roles.Add(registrarRole);
                    context.SaveChanges();
                }

                var registrarUser = new User
                {
                    NationalID = "2222222222",
                    UniversityEmail = "registrar@university.edu",
                    Password = BCrypt.Net.BCrypt.HashPassword("2222222222"),
                    FullName = "مسجل الطلاب",
                    Phone = "+966502222222",
                    Role = RoleType.Registrar,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                context.Users.Add(registrarUser);
                context.SaveChanges();

                var userRole = new UserRole
                {
                    UserID = registrarUser.Id,
                    RoleID = registrarRole.Id,
                    AssignedBy = 1, // SuperAdmin
                    ExpiryDate = DateOnly.FromDateTime(DateTime.Now.AddYears(2)),
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                context.UserRoles.Add(userRole);
                context.SaveChanges();
            }
        }

        private static void ClearExistingData(HUPDbContext context)
        {
            context.UserPagePermissions.RemoveRange(context.UserPagePermissions);
            context.RolePagePermissions.RemoveRange(context.RolePagePermissions);
            context.UserRoles.RemoveRange(context.UserRoles);
            context.Enrollments.RemoveRange(context.Enrollments);
            context.Exams.RemoveRange(context.Exams);
            context.StudentContacts.RemoveRange(context.StudentContacts);
            context.StudentPersonals.RemoveRange(context.StudentPersonals);
            context.Students.RemoveRange(context.Students);
            context.Instructors.RemoveRange(context.Instructors);
            context.Courses.RemoveRange(context.Courses);
            context.Programs.RemoveRange(context.Programs);
            context.Departments.RemoveRange(context.Departments);
            context.Faculties.RemoveRange(context.Faculties);
            context.Users.RemoveRange(context.Users.Where(u => u.NationalID != "0000000000"));
            context.Permissions.RemoveRange(context.Permissions);
            context.Pages.RemoveRange(context.Pages);
            context.Roles.RemoveRange(context.Roles.Where(r => r.RoleName != "SuperAdmin"));

            context.SaveChanges();
        }
    }
}
