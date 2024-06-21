using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface ITrainerRepository
    {
        Task<IEnumerable<Trainer>> GetAllAsync();
        Task<Trainer> GetByIdAsync(Guid id);
        Task CreateAsync(Trainer trainer);
        Task UpdateAsync(Trainer trainer);
        Task DeleteAsync(Guid id);

        Task<IEnumerable<Appointment>> GetAppointmentsByTrainerIdAsync(Guid trainerId);

        Task<IEnumerable<Pet>> GetPetsByTrainerIdAsync(Guid trainerId);

        Task PrescribeTrainingAsync(Training training);

        Task<IEnumerable<TrainingResult>> GetTrainingResultsByTrainerIdAsync(Guid trainerId);

        Task<TrainingRecord> GetTrainingRecordByPetIdAsync(Guid petId);
    }
}
