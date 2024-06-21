using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface ITrainingResultRepository
    {
        Task<IEnumerable<TrainingResult>> GetAllAsync();
        Task<TrainingResult> GetByIdAsync(Guid id);
        Task CreateAsync(TrainingResult trainingRes);
        Task UpdateAsync(TrainingResult trainingRes);
        Task DeleteAsync(Guid id);
    }
}
