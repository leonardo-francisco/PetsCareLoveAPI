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
    public class GenderRepository : IGenderRepository
    {
        private readonly IMongoCollection<Gender> _genderCollection;

        public GenderRepository(PetCareContext context)
        {
            _genderCollection = (IMongoCollection<Gender>?)context.Genders;
        }

        public async Task CreateAsync(Gender gender)
        {
            await _genderCollection.InsertOneAsync(gender);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _genderCollection.DeleteOneAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Gender>> GetAllAsync()
        {
            return await _genderCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Gender> GetByIdAsync(Guid id)
        {
            return await _genderCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Gender gender)
        {
            var filter = Builders<Gender>.Filter.Eq(p => p.Id, gender.Id);
            await _genderCollection.ReplaceOneAsync(filter, gender);
        }
    }
}
