using AutoMapper;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamService(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<ExamDto> GetByIdAsync(Guid id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            return _mapper.Map<ExamDto>(exam);
        }

        public async Task<IEnumerable<ExamDto>> GetAllAsync()
        {
            var exams = await _examRepository.GetAllActiveAsync();
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        public async Task<ExamDto> CreateAsync(ExamCreateDto examDto)
        {
            var exam = _mapper.Map<Exam>(examDto);
            await _examRepository.AddAsync(exam);
            return await GetByIdAsync(exam.Id);
        }

        public async Task<ExamDto> UpdateAsync(Guid id, ExamUpdateDto examDto)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null)
                throw new KeyNotFoundException("Exam not found.");

            _mapper.Map(examDto, exam);
            await _examRepository.UpdateAsync(exam);

            return await GetByIdAsync(exam.Id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _examRepository.DeleteAsync(id);
            return true;
        }
    }
}
