using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task CreateServiceAsync(ServiceDto serviceDto)
        {
            var type = _mapper.Map<PCL.Domain.Entities.Service>(serviceDto);
            await _serviceRepository.CreateServiceAsync(type);
        }

        public async Task DeleteServiceAsync(Guid id)
        {
            var type = await _serviceRepository.GetServiceByIdAsync(id);
            await _serviceRepository.DeleteServiceAsync(type.Id);
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            var types = await _serviceRepository.GetAllServicesAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(types);
        }

        public async Task<ServiceDto> GetServiceByIdAsync(Guid id)
        {
            var type = await _serviceRepository.GetServiceByIdAsync(id);
            return _mapper.Map<ServiceDto>(type);
        }

        public async Task UpdateServiceAsync(ServiceDto serviceDto)
        {
            var type = _mapper.Map<PCL.Domain.Entities.Service>(serviceDto);
            await _serviceRepository.UpdateServiceAsync(type);
        }
    }
}
