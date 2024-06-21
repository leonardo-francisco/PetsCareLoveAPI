using PCL.Application.Dto;
using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Trainer
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerDto>> GetAllTrainersAsync();
        Task<TrainerDto> GetTrainerByIdAsync(Guid id);
        Task CreateTrainerAsync(TrainerDto trainerDto);
        Task UpdateTrainerAsync(TrainerDto trainerDto);
        Task DeleteTrainerAsync(Guid id);

        Task<IEnumerable<AppointmentDto>> GetAppointmentsByTrainerIdAsync(Guid trainerId);

        Task<IEnumerable<PetDto>> GetPetsByTrainerIdAsync(Guid trainerId);

        Task PrescribeTrainingAsync(TrainingDto trainingDto);

        Task<IEnumerable<TrainingResultDto>> GetTrainingResultsByTrainerIdAsync(Guid trainerId);

        Task<TrainingRecordDto> GetTrainingRecordByPetIdAsync(Guid petId);
    }
}
