using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> AttendByServiceTypeAsync(int serviceType)
        {
            var types = await _employeeRepository.AttendByServiceTypeAsync(serviceType);
            return _mapper.Map<IEnumerable<EmployeeDto>>(types);
        }

        public async Task CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            var type = _mapper.Map<PCL.Domain.Entities.Employee>(employeeDto);
            await _employeeRepository.CreateEmployeeAsync(type);
        }

        public async Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto)
        {
            var type = _mapper.Map<PCL.Domain.Entities.Service>(serviceDto);
            await _employeeRepository.CreateServiceAsync(type);
            return serviceDto;
        }

        public async Task DeleteEmployeeAsync(Guid employeeId)
        {
            var type = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            await _employeeRepository.DeleteEmployeeAsync(type.Id);
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var types = await _employeeRepository.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(types);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid employeeId)
        {
            var type = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            return _mapper.Map<EmployeeDto>(type);
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var type = _mapper.Map<PCL.Domain.Entities.Employee>(employeeDto);
            await _employeeRepository.UpdateEmployeeAsync(type);
        }
    }
}
