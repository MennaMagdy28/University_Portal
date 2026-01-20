using HUP.Application.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;
using HUP.Core.Entities.Identity;
using Riok.Mapperly.Abstractions;

namespace HUP.Application.Mappers
{
    [Mapper]
    public static partial class ClassGroupMapper
    {
        [MapProperty(
            [nameof(ClassGroup.CourseOffering), nameof(CourseOffering.Course), nameof(Course.CourseCode)],
            nameof(ClassGroupDto.CourseCode))]
        [MapProperty(
            [nameof(ClassGroup.CourseOffering), nameof(CourseOffering.Course), nameof(Course.CourseName)],
            nameof(ClassGroupDto.CourseName))]
        [MapProperty(
            [nameof(ClassGroup.Instructor), nameof(Instructor.User), nameof(User.FullName)],
            nameof(ClassGroupDto.InstructorName))]

        public static partial ClassGroupDto ToDto(ClassGroup classGroup);

        public static partial List<ClassGroupDto> ToDto(IEnumerable<ClassGroup> classGroups);

        public static partial ClassGroup ToEntity(CreateClassGroupDto dto);

        [MapperIgnoreSource(nameof(UpdateClassGroupDto.Id))]
        public static partial void ToEntity(UpdateClassGroupDto dto, ClassGroup entity);


        [MapperIgnore]
        private static string GetArabicDay(DayOfWeek day)
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

        [MapperIgnore]
        public static ClassGroupDto MapToDtoWithArabic(ClassGroup classGroup)
        {
            var dto = ToDto(classGroup);
            if (dto != null)
            {
                dto.DayOfWeekArabic = GetArabicDay(classGroup.DayOfWeek);
            }
            return dto;
        }
    }
}
