using HUP.Application.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;
using Riok.Mapperly.Abstractions;
using HUP.Application.DTOs.AcademicDtos.Enrollment;

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

        //Mapping for UpdateEnrollmentDto
        // status update
        public static partial Enrollment ToEntityFromUpdateDto(UpdateEnrollmentStatusDto dto);
        public static partial void ToUpdateStatus(UpdateEnrollmentStatusDto dto, Enrollment entity);

        // grades update
        public static partial Enrollment ToEntityFromUpdateGradeDto(UpdateEnrollmentGradesDto dto);
        public static partial void ToUpdateGrades(UpdateEnrollmentGradesDto dto, Enrollment entity);
    }
}
