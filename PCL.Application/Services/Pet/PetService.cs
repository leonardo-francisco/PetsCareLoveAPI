using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Pet
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public PetService(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task CreatePetAsync(PetDto petDto)
        {
            var pet = _mapper.Map<PCL.Domain.Entities.Pet>(petDto);
            await _petRepository.CreateAsync(pet);
        }

        public async Task DeletePetAsync(Guid id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            await _petRepository.DeleteAsync(pet.Id);
        }

        public async Task<IEnumerable<PetDto>> GetAllPetsAsync()
        {
            var pets = await _petRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<PetDto> GetPetByIdAsync(Guid id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            var result = _mapper.Map<PetDto>(pet);
            if (pet != null)
            {
                result.TypeAnimalId = pet.Breed.TypeAnimalId;
            }
            
            return result;
        }

        public async Task UpdatePetAsync(PetDto petDto)
        {
            var pet = _mapper.Map<PCL.Domain.Entities.Pet>(petDto);
            await _petRepository.UpdateAsync(pet);
        }
    }
}
