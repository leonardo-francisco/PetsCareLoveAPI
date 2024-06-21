using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Training
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public TrainingService(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(TrainingDto trainingDto)
        {
            var training = _mapper.Map<PCL.Domain.Entities.Training>(trainingDto);
            await _trainingRepository.CreateAsync(training);
        }

        public async Task DeleteAsync(Guid id)
        {
            var training = await _trainingRepository.GetByIdAsync(id);
            await _trainingRepository.DeleteAsync(training.Id);
        }

        public async Task<IEnumerable<TrainingDto>> GetAllAsync()
        {
            var trainings = await _trainingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TrainingDto>>(trainings);
        }

        public async Task<TrainingDto> GetByIdAsync(Guid id)
        {
            var training = await _trainingRepository.GetByIdAsync(id);
            return _mapper.Map<TrainingDto>(training);
        }

        public async Task UpdateAsync(TrainingDto trainingDto)
        {
            var training = _mapper.Map<PCL.Domain.Entities.Training>(trainingDto);
            await _trainingRepository.UpdateAsync(training);
        }
    }
}
