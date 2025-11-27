using AutoMapper;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;

namespace HUP.Common.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentResponseDto>()
                .ForMember(dest => dest.StudentID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.FacultyName, opt => opt.MapFrom(src => src.Faculty.DisplayName))
                .ForMember(dest => dest.ProgramName, opt => opt.MapFrom(src => src.Program.ProgramName));


            CreateMap<CourseSchedule, StudentTimetableDto>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek.ToString()))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToTimeSpan()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToTimeSpan()))
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.Course.CourseCode))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.User.FullName));

            CreateMap<CourseSchedule, StudentTimetableDto>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek.ToString()))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToTimeSpan()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToTimeSpan()))
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.Course.CourseCode))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.User.FullName));


            CreateMap<CourseSchedule, CourseScheduleDto>()
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.Course.CourseCode))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.User.FullName))
                .ForMember(dest => dest.DayOfWeekArabic, opt => opt.MapFrom(src => GetArabicDay(src.DayOfWeek)))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToTimeSpan()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToTimeSpan()));

            CreateMap<CourseScheduleCreateDto, CourseSchedule>();
            CreateMap<CourseScheduleUpdateDto, CourseSchedule>();
        }

        private string GetArabicDay(DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Sunday => "الأحد",
                DayOfWeek.Monday => "الاثنين",
                DayOfWeek.Tuesday => "الثلاثاء",
                DayOfWeek.Wednesday => "الأربعاء",
                DayOfWeek.Thursday => "الخميس",
                DayOfWeek.Friday => "الجمعة",
                DayOfWeek.Saturday => "السبت",
                _ => day.ToString()
            };
        }
    }
}
