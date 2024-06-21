using MongoDB.Driver;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using PCL.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Infrastructure.Repositories
{
    public class TypeAnimalRepository : ITypeAnimalRepository
    {
        private readonly IMongoCollection<TypeAnimal> _typeAnimalCollection;

        public TypeAnimalRepository(PetCareContext context)
        {
            _typeAnimalCollection = (IMongoCollection<TypeAnimal>?)context.TypeAnimals;
        }

        public async Task CreateAsync(TypeAnimal typeAnimal)
        {
            await _typeAnimalCollection.InsertOneAsync(typeAnimal);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _typeAnimalCollection.DeleteOneAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<TypeAnimal>> GetAllAsync()
        {
            return await _typeAnimalCollection.Find(_ => true).ToListAsync();
        }

        public async Task<TypeAnimal> GetByIdAsync(Guid id)
        {
            return await _typeAnimalCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TypeAnimal typeAnimal)
        {
            var filter = Builders<TypeAnimal>.Filter.Eq(p => p.Id, typeAnimal.Id);
            await _typeAnimalCollection.ReplaceOneAsync(filter, typeAnimal);
        }
    }
}
