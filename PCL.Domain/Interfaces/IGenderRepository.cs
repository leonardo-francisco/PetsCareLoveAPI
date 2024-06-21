using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface IGenderRepository
    {
        Task<IEnumerable<Gender>> GetAllAsync();
        Task<Gender> GetByIdAsync(Guid id);
        Task CreateAsync(Gender gender);
        Task UpdateAsync(Gender gender);
        Task DeleteAsync(Guid id);
    }
}
