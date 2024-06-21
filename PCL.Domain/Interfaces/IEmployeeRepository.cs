using PCL.Domain.Entities;
using PCL.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByIdAsync(Guid employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Guid employeeId);

        Task<Service> CreateServiceAsync(Service service);      
        Task<IEnumerable<Employee>> AttendByServiceTypeAsync(int serviceType);
    }
}
