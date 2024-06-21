using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Pet
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetAllPetsAsync();
        Task<PetDto> GetPetByIdAsync(Guid id);
        Task CreatePetAsync(PetDto petDto);
        Task UpdatePetAsync(PetDto petDto);
        Task DeletePetAsync(Guid id);
    }
}
