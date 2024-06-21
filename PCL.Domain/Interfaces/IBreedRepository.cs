using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface IBreedRepository
    {
        Task<IEnumerable<Breed>> GetAllAsync();
        Task<Breed> GetByIdAsync(Guid id);
        Task CreateAsync(Breed breed);
        Task UpdateAsync(Breed breed);
        Task DeleteAsync(Guid id);
    }
}
