using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Examination
{
    public class ExaminationService : IExaminationService
    {
        private readonly IExaminationRepository _examinationRepository;
        private readonly IMapper _mapper;

        public ExaminationService(IExaminationRepository examinationRepository, IMapper mapper)
        {
            _examinationRepository = examinationRepository;
            _mapper = mapper;
        }

        public async Task<ExaminationDto> GetExaminationByIdAsync(Guid examinationId)
        {
            var exam = await _examinationRepository.GetExaminationByIdAsync(examinationId);
            return _mapper.Map<ExaminationDto>(exam);
        }

        public async Task<IEnumerable<ExaminationDto>> GetExaminationsByPetIdAsync(Guid petId)
        {
            var exams = await _examinationRepository.GetExaminationsByPetIdAsync(petId);
            return _mapper.Map<IEnumerable<ExaminationDto>>(exams);
        }

        public async Task PrescribeExaminationAsync(ExaminationDto examinationDto)
        {
            var exam = _mapper.Map<PCL.Domain.Entities.Examination>(examinationDto);
            await _examinationRepository.PrescribeExaminationAsync(exam);
        }

        public async Task UpdateExaminationAsync(ExaminationDto examinationDto)
        {
            var exam = _mapper.Map<PCL.Domain.Entities.Examination>(examinationDto);
            await _examinationRepository.UpdateExaminationAsync(exam);
        }

        public async Task UpdateExaminationResultAsync(ExaminationResultDto examinationResultDto)
        {
            var examRes = _mapper.Map<PCL.Domain.Entities.ExaminationResult>(examinationResultDto);
            await _examinationRepository.UpdateExaminationResultAsync(examRes);
        }
    }
}
