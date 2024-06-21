using MongoDB.Driver;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using PCL.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly PetCareContext _context;

        public ServiceRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CreateServiceAsync(Service service)
        {
            await _context.Services.InsertOneAsync(service);
        }

        public async Task DeleteServiceAsync(Guid id)
        {
            await _context.Services.DeleteOneAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _context.Services.Find(_ => true).ToListAsync();
        }

        public async Task<Service> GetServiceByIdAsync(Guid id)
        {
            return await _context.Services.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateServiceAsync(Service service)
        {
            var filter = Builders<Service>.Filter.Eq(p => p.Id, service.Id);
            await _context.Services.ReplaceOneAsync(filter, service);
        }
    }
}
