using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface ITrainingRecordRepository
    {
        Task<IEnumerable<TrainingRecord>> GetAllAsync();
        Task<TrainingRecord> GetByIdAsync(Guid id);
        Task CreateAsync(TrainingRecord trainingRec);
        Task UpdateAsync(TrainingRecord trainingRec);
        Task DeleteAsync(Guid id);
    }
}
