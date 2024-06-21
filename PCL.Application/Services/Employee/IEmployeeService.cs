using PCL.Application.Dto;
using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Employee
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetEmployeeByIdAsync(Guid employeeId);
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task CreateEmployeeAsync(EmployeeDto employeeDto);
        Task UpdateEmployeeAsync(EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(Guid employeeId);

        Task<ServiceDto> CreateServiceAsync(ServiceDto serviceDto);
        Task<IEnumerable<EmployeeDto>> AttendByServiceTypeAsync(int serviceType);
    }
}
