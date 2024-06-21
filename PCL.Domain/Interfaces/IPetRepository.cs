using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet> GetByIdAsync(Guid id);    
        Task CreateAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(Guid id);
    }
}
