using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.TrainingResult
{
    public interface ITrainingResultService
    {
        Task<IEnumerable<TrainingResultDto>> GetAllAsync();
        Task<TrainingResultDto> GetByIdAsync(Guid id);
        Task CreateAsync(TrainingResultDto trainingResDto);
        Task UpdateAsync(TrainingResultDto trainingResDto);
        Task DeleteAsync(Guid id);
    }
}
