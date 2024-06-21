using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface ITrainingRepository
    {
        Task<IEnumerable<Training>> GetAllAsync();
        Task<Training> GetByIdAsync(Guid id);
        Task CreateAsync(Training training);
        Task UpdateAsync(Training training);
        Task DeleteAsync(Guid id);
    }
}
