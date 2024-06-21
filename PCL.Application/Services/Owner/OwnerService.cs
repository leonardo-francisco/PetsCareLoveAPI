using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Owner
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerService(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task AddOwnerAsync(OwnerDto ownerDto)
        {
            var owner = _mapper.Map<PCL.Domain.Entities.Owner>(ownerDto);
            await _ownerRepository.AddAsync(owner);
        }

        public async Task DeleteOwnerAsync(Guid id)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);
            await _ownerRepository.DeleteAsync(owner.Id);
        }

        public async Task<IEnumerable<OwnerDto>> GetAllOwnersAsync()
        {
            var owners = await _ownerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OwnerDto>>(owners);
        }

        public async Task<OwnerDto> GetOwnerByIdAsync(Guid id)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);
            return _mapper.Map<OwnerDto>(owner);
        }

        public async Task UpdateOwnerAsync(OwnerDto ownerDto)
        {
            var owner = _mapper.Map<PCL.Domain.Entities.Owner>(ownerDto);
            await _ownerRepository.UpdateAsync(owner);
        }
    }
}
