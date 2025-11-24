using HUP.Application.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;
using Riok.Mapperly.Abstractions;

namespace HUP.Application.Mappers
{
    [Mapper]
    public static partial class EnrollmentMapper
    {
        // Mapping for CreateEnrollmentDto
        public static partial Enrollment ToEntityFromCreateDto(CreateEnrollmentDto dto);
        public static partial CreateEnrollmentDto ToCreateDto(Enrollment entity);

        // Mapping for EnrollmentResponseDto
        public static partial Enrollment ToEntityFromResponseDto(EnrollmentResponseDto dto);
        public static partial EnrollmentResponseDto ToResponseDto(Enrollment entity);
    }
}
