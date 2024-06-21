using MongoDB.Driver;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using PCL.Domain.Utils;
using PCL.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PetCareContext _context;
        private readonly PasswordHasher _passwordHasher;

        public UserRepository(PetCareContext context, PasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Users.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.Find(p => p.Email == email).FirstOrDefaultAsync();           
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq(p => p.Id, user.Id);
            await _context.Users.ReplaceOneAsync(filter, user);
        }

        public async Task UpdatePasswordAsync(Guid userId, string newPassword)
        {
            var user = await _context.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(newPassword);
                await _context.Users.ReplaceOneAsync(u => u.Id == userId, user);
            }
        }
    }
}
