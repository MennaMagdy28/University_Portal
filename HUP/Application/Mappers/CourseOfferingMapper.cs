using HUP.Application.DTOs.AcademicDtos.CourseOffering;
using Riok.Mapperly.Abstractions;
using HUP.Core.Entities.Academics;

namespace HUP.Application.Mappers
{
    [Mapper]
    public static partial class CourseOfferingMapper
    {
        [MapProperty(nameof(CourseOffering.Course.CourseCode), nameof(CourseOfferingDto.CourseCode))]
        [MapProperty(nameof(CourseOffering.Course.CourseName), nameof(CourseOfferingDto.CourseName))]
        [MapProperty(nameof(CourseOffering.Course.Credits), nameof(CourseOfferingDto.Credits))]
        public static partial CourseOfferingDto ToDto(CourseOffering courseOffering);

        public static partial List<CourseOfferingDto> ToDto(IEnumerable<CourseOffering> offerings);

        public static partial CourseOffering ToEntity(CreateCourseOfferingDto dto);
    }
}
