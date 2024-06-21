using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Breed
{
    public class BreedService : IBreedService
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;

        public BreedService(IBreedRepository breedRepository, IMapper mapper)
        {
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        public async Task CreateBreedAsync(BreedDto breedDto)
        {
            var breed = _mapper.Map<PCL.Domain.Entities.Breed>(breedDto);
            await _breedRepository.CreateAsync(breed);
        }

        public async Task DeleteBreedAsync(Guid id)
        {
            var breed = await _breedRepository.GetByIdAsync(id);
            await _breedRepository.DeleteAsync(breed.Id);
        }

        public async Task<IEnumerable<BreedDto>> GetAllBreedsAsync()
        {
            var breeds = await _breedRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BreedDto>>(breeds);
        }

        public async Task<BreedDto> GetBreedByIdAsync(Guid id)
        {
            var breed = await _breedRepository.GetByIdAsync(id);
            return _mapper.Map<BreedDto>(breed);
        }

        public async Task UpdateBreedAsync(BreedDto breedDto)
        {
            var breed = _mapper.Map<PCL.Domain.Entities.Breed>(breedDto);
            await _breedRepository.UpdateAsync(breed);
        }
    }
}
