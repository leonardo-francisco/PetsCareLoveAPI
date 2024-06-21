using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.TrainingRecord
{
    public interface ITrainingRecordService
    {
        Task<IEnumerable<TrainingRecordDto>> GetAllAsync();
        Task<TrainingRecordDto> GetByIdAsync(Guid id);
        Task CreateAsync(TrainingRecordDto trainingRecDto);
        Task UpdateAsync(TrainingRecordDto trainingRecDto);
        Task DeleteAsync(Guid id);
    }
}
