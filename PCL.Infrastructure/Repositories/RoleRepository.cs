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
    public class RoleRepository : IRoleRepository
    {
        private readonly PetCareContext _context;

        public RoleRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Role role)
        {
            await _context.Roles.InsertOneAsync(role);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Roles.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.Find(_ => true).ToListAsync();
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _context.Roles.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            var filter = Builders<Role>.Filter.Eq(p => p.Id, role.Id);
            await _context.Roles.ReplaceOneAsync(filter, role);
        }
    }
}
