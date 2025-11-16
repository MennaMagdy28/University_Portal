using HUP.Core.Entities.Academics;
using Riok.Mapperly.Abstractions;
using HUP.Core.DTOs.AcademicDtos;

namespace HUP.Application.Mappers
{
    [Mapper]
    public static partial class EnrollmentMapper
    {

        public static partial Enrollment ToEntity(CreateEnrollmentDto dto);
        public static partial CreateEnrollmentDto ToDto(Enrollment entity);
    }
}
