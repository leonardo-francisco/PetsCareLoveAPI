using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.TypeAnimal
{
    public class TypeAnimalService : ITypeAnimalService
    {
        private readonly ITypeAnimalRepository _typeAnimalRepository;
        private readonly IMapper _mapper;

        public TypeAnimalService(ITypeAnimalRepository typeAnimalRepository, IMapper mapper)
        {
            _typeAnimalRepository = typeAnimalRepository;
            _mapper = mapper;
        }

        public async Task CreateTypeAnimalAsync(TypeAnimalDto typeAnimalDto)
        {
            var type = _mapper.Map<PCL.Domain.Entities.TypeAnimal>(typeAnimalDto);
            await _typeAnimalRepository.CreateAsync(type);
        }

        public async Task DeleteTypeAnimalAsync(Guid id)
        {
            var type = await _typeAnimalRepository.GetByIdAsync(id);
            await _typeAnimalRepository.DeleteAsync(type.Id);
        }

        public async Task<IEnumerable<TypeAnimalDto>> GetAllTypeAnimalsAsync()
        {
            var types = await _typeAnimalRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TypeAnimalDto>>(types);
        }

        public async Task<TypeAnimalDto> GetTypeAnimalByIdAsync(Guid id)
        {
            var type = await _typeAnimalRepository.GetByIdAsync(id);
            return _mapper.Map<TypeAnimalDto>(type);
        }

        public async Task UpdateTypeAnimalAsync(TypeAnimalDto typeAnimalDto)
        {
            var type = _mapper.Map<PCL.Domain.Entities.TypeAnimal>(typeAnimalDto);
            await _typeAnimalRepository.UpdateAsync(type);
        }
    }
}
