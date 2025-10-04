using AutoMapper;
using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.DTOs.CommonDtos;
using HUP.Core.DTOs.ServiceDtos;
using HUP.Core.DTOs.UserDtos;
using HUP.Core.Models.AcademicModels;
using HUP.Core.Models.ServiceModels;
using HUP.Core.Models.UserModels;

namespace HUP
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            // User
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.RoleName)))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src =>
                    src.UserPagePermissions.Select(up => $"{up.Page.PageName}:{up.Permission.PermissionName}")));
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UpdateUserDto, User>();

            // Role
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src =>
                    src.RolePagePermissions.Select(rp => new RolePagePermissionDto
                    {
                        PageName = rp.Page.PageName,
                        PermissionName = rp.Permission.PermissionName
                    })));
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();

            CreateMap<Page, PageDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src =>
                    src.RolePagePermissions.Select(rp => rp.Permission.PermissionName)));

            CreateMap<Permission, PermissionDto>();

            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
            CreateMap<RegisterStudentDto, Student>();

            CreateMap<Instructor, InstructorDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));
            CreateMap<CreateInstructorDto, Instructor>();

            CreateMap<Course, CourseDto>();
            CreateMap<CreateCourseDto, Course>();

            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.User.FullName))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName));
            CreateMap<RegisterCourseDto, Enrollment>();

            CreateMap<Exam, ExamDto>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName));
            CreateMap<CreateExamDto, Exam>();
        }
    }
}
