using MongoDB.Driver;
using PCL.Domain.Entities;
using PCL.Domain.Enums;
using PCL.Domain.Interfaces;
using PCL.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PetCareContext _context;

        public EmployeeRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> AttendByServiceTypeAsync(int serviceType)
        {
            return await _context.Employees.Find(g => g.ServiceType == serviceType).ToListAsync();
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _context.Employees.InsertOneAsync(employee);
        }


        public async Task<Service> CreateServiceAsync(Service service)
        {
            await _context.Services.InsertOneAsync(service);
            return service;
        }

        public async Task DeleteEmployeeAsync(Guid employeeId)
        {
            await _context.Employees.DeleteOneAsync(g => g.Id == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Find(_ => true).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid employeeId)
        {
            return await _context.Employees.Find(g => g.Id == employeeId).FirstOrDefaultAsync();
        }
    
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var filter = Builders<Employee>.Filter.Eq(p => p.Id, employee.Id);
            await _context.Employees.ReplaceOneAsync(filter, employee);
        }
    }
}
