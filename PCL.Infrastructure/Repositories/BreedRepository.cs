using MongoDB.Driver;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using PCL.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PCL.Infrastructure.Repositories
{
    public class BreedRepository : IBreedRepository
    {
        private readonly PetCareContext _context;

        public BreedRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Breed breed)
        {
            await _context.Breeds.InsertOneAsync(breed);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Breeds.DeleteOneAsync(breed => breed.Id == id);
        }

        public async Task<IEnumerable<Breed>> GetAllAsync()
        {
            var breeds = await _context.Breeds.Aggregate()
            .Lookup<Breed, TypeAnimal, Breed>(
                _context.TypeAnimals,
                breed => breed.TypeAnimalId,
                typeAnimal => typeAnimal.Id,
                breed => breed.TypeAnimal)
            .Unwind<Breed, Breed>(breed => breed.TypeAnimal)
            .ToListAsync();

            return breeds;
        }

        public async Task<Breed> GetByIdAsync(Guid id)
        {
            //return await _context.Breeds.Find(breed => breed.Id == id).FirstOrDefaultAsync();
            var filter = Builders<Breed>.Filter.Eq(p => p.Id, id);
            var breed = await _context.Breeds.Find(filter).FirstOrDefaultAsync();

            if (breed != null)
            { 
                var animal = await _context.TypeAnimals.Find(f => f.Id == breed.TypeAnimalId).FirstOrDefaultAsync();
                breed.TypeAnimal = animal;               
            }

            return breed;
        }

        public async Task UpdateAsync(Breed breed)
        {
            var filter = Builders<Breed>.Filter.Eq(p => p.Id, breed.Id);
            await _context.Breeds.ReplaceOneAsync(filter,breed);
        }
    }
}
