using HUP.Core.Entities.Academics;
using Riok.Mapperly.Abstractions;
using HUP.Core.DTOs.AcademicDtos;

namespace HUP.Application.Mappers
{
    [Mapper]
    public partial class EnrollmentMapper
    {

        public partial Enrollment ToEntity(CreateEnrollmentDto dto);
        public partial CreateEnrollmentDto ToDto(Enrollment entity);
    }
}
