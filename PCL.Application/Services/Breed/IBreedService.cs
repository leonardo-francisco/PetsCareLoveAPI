using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Breed
{
    public interface IBreedService
    {
        Task<IEnumerable<BreedDto>> GetAllBreedsAsync();
        Task<BreedDto> GetBreedByIdAsync(Guid id);
        Task CreateBreedAsync(BreedDto breedDto);
        Task UpdateBreedAsync(BreedDto breedDto);
        Task DeleteBreedAsync(Guid id);
    }
}
