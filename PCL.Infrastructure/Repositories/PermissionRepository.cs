using DnsClient;
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
    public class PermissionRepository : IPermissionRepository
    {
        private readonly PetCareContext _context;

        public PermissionRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Permission permission)
        {
            await _context.Permissions.InsertOneAsync(permission);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Permissions.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _context.Permissions.Find(_ => true).ToListAsync();
        }

        public async Task<Permission> GetByIdAsync(Guid id)
        {
            return await _context.Permissions.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Permission permission)
        {
            var filter = Builders<Permission>.Filter.Eq(p => p.Id, permission.Id);
            await _context.Permissions.ReplaceOneAsync(filter, permission);
        }
    }
}
