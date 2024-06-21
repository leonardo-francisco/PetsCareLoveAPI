using MongoDB.Driver;
using NLog.Filters;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PetCareContext _context;

        public CategoryRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.InsertOneAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Categories.DeleteOneAsync(breed => breed.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Find(_ => true).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await _context.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            var filter = Builders<Category>.Filter.Eq(p => p.Id, category.Id);
            await _context.Categories.ReplaceOneAsync(filter,category);
        }
    }
}
