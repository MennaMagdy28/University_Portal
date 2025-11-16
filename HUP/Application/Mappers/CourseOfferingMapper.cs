using Riok.Mapperly.Abstractions;
using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;

namespace HUP.Application.Mappers
{
    [Mapper]
    public partial class CourseOfferingMapper
    {
        [MapProperty(nameof(CourseOffering.Course.CourseCode), nameof(CourseOfferingDto.CourseCode))]
        [MapProperty(nameof(CourseOffering.Course.CourseName), nameof(CourseOfferingDto.CourseName))]
        [MapProperty(nameof(CourseOffering.Course.Credits), nameof(CourseOfferingDto.Credits))]
        public partial CourseOfferingDto ToDto(CourseOffering courseOffering);
    }
}

