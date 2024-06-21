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
    public class ProductRepository : IProductRepository
    {
        private readonly PetCareContext _context;

        public ProductRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Products.DeleteOneAsync(breed => breed.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAndPetIdAsync(Guid categoryId, Guid petId)
        {
            var products = await _context.Products.Find(c => c.CategoryId == categoryId && c.PetId == petId).ToListAsync();
            return products;
        }

        public async Task<Product> GetByCategoryIdAsync(Guid id)
        {
            var product = await _context.Products.Find(c => c.CategoryId == id).FirstOrDefaultAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.Find(c => c.Id == id).FirstOrDefaultAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            await _context.Products.ReplaceOneAsync(filter, product);
        }
    }
}
