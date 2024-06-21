using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Gender
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public GenderService(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }

        public async Task CreateGenderAsync(GenderDto genderDto)
        {
            var gender = _mapper.Map<PCL.Domain.Entities.Gender>(genderDto);
            await _genderRepository.CreateAsync(gender);
        }

        public async Task DeleteGenderAsync(Guid id)
        {
            var gender = await _genderRepository.GetByIdAsync(id);
            await _genderRepository.DeleteAsync(gender.Id);
        }

        public async Task<IEnumerable<GenderDto>> GetAllGendersAsync()
        {
            var genders = await _genderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GenderDto>>(genders);
        }

        public async Task<GenderDto> GetGenderByIdAsync(Guid id)
        {
            var gender = await _genderRepository.GetByIdAsync(id);
            return _mapper.Map<GenderDto>(gender);
        }

        public async Task UpdateGenderAsync(GenderDto genderDto)
        {
            var gender = _mapper.Map<PCL.Domain.Entities.Gender>(genderDto);
            await _genderRepository.UpdateAsync(gender);
        }
    }
}
