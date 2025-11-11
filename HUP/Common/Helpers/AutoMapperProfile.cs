using AutoMapper;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.DTOs.UserDtos;
using HUP.Core.Enums;
using HUP.Core.Models.AcademicModels;
using HUP.Core.Models.UserModels;

namespace HUP.Common.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User Mappings
            CreateMap<User, UserResponseDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            // Student Mappings
            CreateMap<Student, StudentResponseDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.FacultyName, opt => opt.MapFrom(src => src.Faculty.FacultyName))
                .ForMember(dest => dest.ProgramName, opt => opt.MapFrom(src => src.Program.ProgramName));

            CreateMap<StudentCreateDto, User>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => RoleType.Student));

            // Academic Mappings
            CreateMap<FacultyCreateDto, Faculty>();
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<ProgramCreateDto, Program>();
            CreateMap<CourseCreateDto, Course>();
        }
    }
}
