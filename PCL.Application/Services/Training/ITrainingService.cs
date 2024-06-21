using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Training
{
    public interface ITrainingService
    {
        Task<IEnumerable<TrainingDto>> GetAllAsync();
        Task<TrainingDto> GetByIdAsync(Guid id);
        Task CreateAsync(TrainingDto trainingDto);
        Task UpdateAsync(TrainingDto trainingDto);
        Task DeleteAsync(Guid id);
    }
}
