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
    public class PetRepository : IPetRepository
    {
        private readonly PetCareContext _context;
        private readonly ImageHelper _imageHelper;

        public PetRepository(PetCareContext context, ImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
        }

        public async Task CreateAsync(Pet pet)
        {
            await _context.Pets.InsertOneAsync(pet);
            var owner = await _context.Owners.Find(p => p.Id == pet.OwnerId).FirstOrDefaultAsync();
            owner.PetIds.Add(pet.Id);
            await _context.Owners.ReplaceOneAsync(o => o.Id == pet.OwnerId, owner);

            if (!string.IsNullOrEmpty(pet.Photo))
            {
                string directoryPath = Path.Combine("wwwroot", "images", "pets");
                Directory.CreateDirectory(directoryPath); 

                string imagePath = Path.Combine(directoryPath, $"{pet.Id}.jpg");

                await _imageHelper.DownloadAndSaveImageAsync(pet.Photo, imagePath);

                // Update the Photo property to the new local path
                pet.Photo = $"/images/pets/{pet.Id}.jpg";
                var updateDefinition = Builders<Pet>.Update.Set(o => o.Photo, pet.Photo);
                await _context.Pets.UpdateOneAsync(o => o.Id == pet.Id, updateDefinition);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Pets.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            //var pets = await _context.Pets.Aggregate()
            //// Join with Breeds collection
            //.Lookup<Pet, Breed, Pet>(
            //    _context.Breeds,
            //    pet => pet.BreedId,
            //    breed => breed.Id,
            //    pet => pet.Breed)
            //.Unwind<Pet, Pet>(pet => pet.Breed)
            //// Join with TypeAnimals collection
            //.Lookup<Pet, TypeAnimal, Pet>(
            //    _context.TypeAnimals,
            //    pet => pet.Breed.TypeAnimalId,
            //    typeAnimal => typeAnimal.Id,
            //    pet => pet.Breed.TypeAnimal)
            //.Unwind<Pet, Pet>(pet => pet.Breed.TypeAnimal)
            //// Join with Genders collection
            //.Lookup<Pet, Gender, Pet>(
            //    _context.Genders,
            //    pet => pet.GenderId,
            //    gender => gender.Id,
            //    pet => pet.Gender)
            //.Unwind<Pet, Pet>(pet => pet.Gender)
            //.ToListAsync();

            var pets = await _context.Pets.Find(_ => true).ToListAsync();

            foreach (var item in pets)
            {
                var owner = await _context.Owners.Find(o => o.Id == item.OwnerId).FirstOrDefaultAsync();
                var breed = await _context.Breeds.Find(b => b.Id == item.BreedId).FirstOrDefaultAsync();
                var animal = await _context.TypeAnimals.Find(f => f.Id == breed.TypeAnimalId).FirstOrDefaultAsync();
                item.Breed = breed;
                item.Breed.TypeAnimal = animal;
                item.Owner = owner;

                var gender = await _context.Genders.Find(b => b.Id == item.GenderId).FirstOrDefaultAsync();
                item.Gender = gender;
            }

            return pets;
        }

        public async Task<Pet> GetByIdAsync(Guid id)
        {           
            var pet = await _context.Pets.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (pet != null)
            {
                //var animal = await _context.TypeAnimals.Find(f => f.Id == pet.TypeAnimalId).FirstOrDefaultAsync();
                //pet.TypeAnimal = animal;
                var owner = await _context.Owners.Find(o => o.Id == pet.OwnerId).FirstOrDefaultAsync();
                var breed = await _context.Breeds.Find(b => b.Id == pet.BreedId).FirstOrDefaultAsync();
                var animal = await _context.TypeAnimals.Find(f => f.Id == breed.TypeAnimalId).FirstOrDefaultAsync();
                pet.Owner = owner;
                pet.Breed = breed;
                pet.Breed.TypeAnimal = animal;

                var gender = await _context.Genders.Find(b => b.Id == pet.GenderId).FirstOrDefaultAsync();
                pet.Gender = gender;
            }

            return pet;
        }
       
        public async Task UpdateAsync(Pet pet)
        {
            //var filter = Builders<Pet>.Filter.Eq(p => p.Id, pet.Id);
            //await _context.Pets.ReplaceOneAsync(filter, pet);
            var existingPet = await _context.Pets.Find(o => o.Id == pet.Id).FirstOrDefaultAsync();

            if (existingPet == null)
            {
                throw new Exception("Pet not found");
            }

            bool photoChanged = existingPet.Photo != pet.Photo;

            if (photoChanged && !string.IsNullOrEmpty(pet.Photo))
            {
                string directoryPath = Path.Combine("wwwroot", "images", "pets");
                Directory.CreateDirectory(directoryPath);

                string imagePath = Path.Combine(directoryPath, $"{pet.Id}.jpg");
                
                await _imageHelper.DownloadAndSaveImageAsync(pet.Photo, imagePath);
                
                pet.Photo = $"/images/pets/{pet.Id}.jpg";
            }

            var updateDefinition = Builders<Pet>.Update
                .Set(o => o.Name, pet.Name)
                .Set(o => o.DateOfBirth, pet.DateOfBirth)
                .Set(o => o.BreedId, pet.BreedId)
                .Set(o => o.GenderId, pet.GenderId);

            if (photoChanged)
            {
                updateDefinition = updateDefinition.Set(o => o.Photo, pet.Photo);
            }

            await _context.Pets.UpdateOneAsync(o => o.Id == pet.Id, updateDefinition);
        }
      
    }
}
