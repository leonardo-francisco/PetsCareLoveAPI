using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.TrainingResult
{
    public class TrainingResultService : ITrainingResultService
    {
        private readonly ITrainingResultRepository _trainingResultRepository;
        private readonly IMapper _mapper;

        public TrainingResultService(ITrainingResultRepository trainingResultRepository, IMapper mapper)
        {
            _trainingResultRepository = trainingResultRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(TrainingResultDto trainingResDto)
        {
            var training = _mapper.Map<PCL.Domain.Entities.TrainingResult>(trainingResDto);
            await _trainingResultRepository.CreateAsync(training);
        }

        public async Task DeleteAsync(Guid id)
        {
            var training = await _trainingResultRepository.GetByIdAsync(id);
            await _trainingResultRepository.DeleteAsync(training.Id);
        }

        public async Task<IEnumerable<TrainingResultDto>> GetAllAsync()
        {
            var trainings = await _trainingResultRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TrainingResultDto>>(trainings);
        }

        public async Task<TrainingResultDto> GetByIdAsync(Guid id)
        {
            var training = await _trainingResultRepository.GetByIdAsync(id);
            return _mapper.Map<TrainingResultDto>(training);
        }

        public async Task UpdateAsync(TrainingResultDto trainingResDto)
        {
            var training = _mapper.Map<PCL.Domain.Entities.TrainingResult>(trainingResDto);
            await _trainingResultRepository.UpdateAsync(training);
        }
    }
}
