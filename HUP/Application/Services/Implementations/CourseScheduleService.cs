using AutoMapper;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations
{
    public class CourseScheduleService : ICourseScheduleService
    {
        private readonly ICourseScheduleRepository _courseScheduleRepository;
        private readonly IMapper _mapper;

        public CourseScheduleService(
            ICourseScheduleRepository courseScheduleRepository,
            IMapper mapper)
        {
            _courseScheduleRepository = courseScheduleRepository;
            _mapper = mapper;
        }

        public async Task<CourseScheduleDto> GetByIdAsync(Guid id)
        {
            var schedule = await _courseScheduleRepository.GetByIdAsync(id);
            return _mapper.Map<CourseScheduleDto>(schedule);
        }

        public async Task<IEnumerable<CourseScheduleDto>> GetAllAsync()
        {
            var schedules = await _courseScheduleRepository.GetByStudentAsync(Guid.Empty);
            return _mapper.Map<IEnumerable<CourseScheduleDto>>(schedules);
        }

        public async Task<CourseScheduleDto> CreateAsync(CourseScheduleCreateDto scheduleDto)
        {
            var schedule = _mapper.Map<CourseSchedule>(scheduleDto);
            await _courseScheduleRepository.AddAsync(schedule);
            return await GetByIdAsync(schedule.Id);
        }

        public async Task<CourseScheduleDto> UpdateAsync(Guid id, CourseScheduleUpdateDto scheduleDto)
        {
            var schedule = await _courseScheduleRepository.GetByIdAsync(id);
            if (schedule == null)
                throw new KeyNotFoundException("Course schedule not found.");

            _mapper.Map(scheduleDto, schedule);
            await _courseScheduleRepository.UpdateAsync(schedule);

            return await GetByIdAsync(schedule.Id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _courseScheduleRepository.DeleteAsync(id);
            return true;
        }
    }
}
