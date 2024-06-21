using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Trainer
{
    public class TrainerService: ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public TrainerService(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task CreateTrainerAsync(TrainerDto trainerDto)
        {
            var trng = _mapper.Map<PCL.Domain.Entities.Trainer>(trainerDto);
            await _trainerRepository.CreateAsync(trng);
        }

        public async Task DeleteTrainerAsync(Guid id)
        {
            var type = await _trainerRepository.GetByIdAsync(id);
            await _trainerRepository.DeleteAsync(type.Id);
        }

        public async Task<IEnumerable<TrainerDto>> GetAllTrainersAsync()
        {
            var types = await _trainerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TrainerDto>>(types);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByTrainerIdAsync(Guid trainerId)
        {
            var appt = await _trainerRepository.GetAppointmentsByTrainerIdAsync(trainerId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appt);
        }

        public async Task<IEnumerable<PetDto>> GetPetsByTrainerIdAsync(Guid trainerId)
        {
            var pets = await _trainerRepository.GetPetsByTrainerIdAsync(trainerId);
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<TrainerDto> GetTrainerByIdAsync(Guid id)
        {
            var trg = await _trainerRepository.GetByIdAsync(id);
            return _mapper.Map<TrainerDto>(trg);
        }

        public async Task<TrainingRecordDto> GetTrainingRecordByPetIdAsync(Guid petId)
        {
            var trg = await _trainerRepository.GetTrainingRecordByPetIdAsync(petId);
            return _mapper.Map<TrainingRecordDto>(trg);
        }

        public async Task<IEnumerable<TrainingResultDto>> GetTrainingResultsByTrainerIdAsync(Guid trainerId)
        {
            var pets = await _trainerRepository.GetTrainingResultsByTrainerIdAsync(trainerId);
            return _mapper.Map<IEnumerable<TrainingResultDto>>(pets);
        }

        public async Task PrescribeTrainingAsync(TrainingDto trainingDto)
        {
            var trng = _mapper.Map<PCL.Domain.Entities.Training>(trainingDto);
            await _trainerRepository.PrescribeTrainingAsync(trng);
        }

        public async Task UpdateTrainerAsync(TrainerDto trainerDto)
        {
            var trng = _mapper.Map<PCL.Domain.Entities.Trainer>(trainerDto);
            await _trainerRepository.UpdateAsync(trng);
        }
    }
}
