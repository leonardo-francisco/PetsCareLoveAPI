using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface ITypeAnimalRepository
    {
        Task<IEnumerable<TypeAnimal>> GetAllAsync();
        Task<TypeAnimal> GetByIdAsync(Guid id);
        Task CreateAsync(TypeAnimal typeAnimal);
        Task UpdateAsync(TypeAnimal typeAnimal);
        Task DeleteAsync(Guid id);
    }
}
