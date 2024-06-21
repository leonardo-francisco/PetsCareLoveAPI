using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.TrainingRecord
{
    public class TrainingRecordService : ITrainingRecordService
    {
        private readonly ITrainingRecordRepository _trainingRecordRepository;
        private readonly IMapper _mapper;

        public TrainingRecordService(ITrainingRecordRepository trainingRecordRepository, IMapper mapper)
        {
            _trainingRecordRepository = trainingRecordRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(TrainingRecordDto trainingRecDto)
        {
            var training = _mapper.Map<PCL.Domain.Entities.TrainingRecord>(trainingRecDto);
            await _trainingRecordRepository.CreateAsync(training);
        }

        public async Task DeleteAsync(Guid id)
        {
            var training = await _trainingRecordRepository.GetByIdAsync(id);
            await _trainingRecordRepository.DeleteAsync(training.Id);
        }

        public async Task<IEnumerable<TrainingRecordDto>> GetAllAsync()
        {
            var trainings = await _trainingRecordRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TrainingRecordDto>>(trainings);
        }

        public async Task<TrainingRecordDto> GetByIdAsync(Guid id)
        {
            var training = await _trainingRecordRepository.GetByIdAsync(id);
            return _mapper.Map<TrainingRecordDto>(training);
        }

        public async Task UpdateAsync(TrainingRecordDto trainingRecDto)
        {
            var training = _mapper.Map<PCL.Domain.Entities.TrainingRecord>(trainingRecDto);
            await _trainingRecordRepository.UpdateAsync(training);
        }
    }
}
