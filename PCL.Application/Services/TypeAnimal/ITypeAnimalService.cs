using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.TypeAnimal
{
    public interface ITypeAnimalService
    {
        Task<IEnumerable<TypeAnimalDto>> GetAllTypeAnimalsAsync();
        Task<TypeAnimalDto> GetTypeAnimalByIdAsync(Guid id);
        Task CreateTypeAnimalAsync(TypeAnimalDto typeAnimalDto);
        Task UpdateTypeAnimalAsync(TypeAnimalDto typeAnimalDto);
        Task DeleteTypeAnimalAsync(Guid id);
    }
}
