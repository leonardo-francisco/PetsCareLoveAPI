using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetUserByEmailAsync(string email);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task UpdatePasswordAsync(Guid userId, string newPassword);
        Task DeleteAsync(Guid id);
    }
}
