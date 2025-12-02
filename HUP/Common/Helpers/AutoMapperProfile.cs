using AutoMapper;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;
using HUP.Core.Enums;

namespace HUP.Common.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentResponseDto>()
                .ForMember(dest => dest.StudentID, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName));
                //.ForMember(dest => dest.FacultyName, opt => opt.MapFrom(src => src.Faculty.DisplayName))
                //.ForMember(dest => dest.ProgramName, opt => opt.MapFrom(src => src.Program.ProgramName));


            CreateMap<CourseSchedule, StudentTimetableDto>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek.ToString()))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToTimeSpan()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToTimeSpan()))
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.CourseOffering.Course.CourseCode))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseOffering.Course.CourseName))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.User.FullName));

            CreateMap<CourseSchedule, StudentTimetableDto>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek.ToString()))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToTimeSpan()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToTimeSpan()))
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.CourseOffering.Course.CourseCode))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseOffering.Course.CourseName))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.User.FullName));


            CreateMap<CourseSchedule, CourseScheduleDto>()
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.CourseOffering.Course.CourseCode))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseOffering.Course.CourseName))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.User.FullName))
                .ForMember(dest => dest.DayOfWeekArabic, opt => opt.MapFrom(src => GetArabicDay(src.DayOfWeek)))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToTimeSpan()))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToTimeSpan()));

            CreateMap<CourseScheduleCreateDto, CourseSchedule>();
            CreateMap<CourseScheduleUpdateDto, CourseSchedule>();


            CreateMap<Exam, ExamDto>()
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.CourseOffering.Course.CourseCode))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseOffering.Course.CourseName))
                .ForMember(dest => dest.ExamTypeArabic, opt => opt.MapFrom(src => GetArabicExamType(src.ExamType)))
                .ForMember(dest => dest.ExamDate, opt => opt.MapFrom(src => src.ExamDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.ExamTime, opt => opt.MapFrom(src => src.ExamTime.ToTimeSpan()))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => ExtractRoomNumber(src.Location)));
                //.ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src =>
                //    src.CourseOffering.Department.Instructors.FirstOrDefault().User.FullName ?? "غير محدد"));

            CreateMap<ExamCreateDto, Exam>();
            CreateMap<ExamUpdateDto, Exam>();
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

        private string GetArabicExamType(ExamType examType)
        {
            return examType switch
            {
                ExamType.Midterm => "امتحان منتصف الفصل",
                ExamType.Final => "امتحان نهاية الفصل",
                _ => examType.ToString()
            };
        }

        private string ExtractRoomNumber(string location)
        {
            if (string.IsNullOrEmpty(location))
                return "غير محدد";

            var roomMatch = System.Text.RegularExpressions.Regex.Match(location, @"قاعة\s*(\d+)");
            return roomMatch.Success ? $"قاعة {roomMatch.Groups[1].Value}" : location;
        }
    }
}
