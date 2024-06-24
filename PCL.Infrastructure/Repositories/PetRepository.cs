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
        private readonly PhotoTransfer _photoTransfer;

        public PetRepository(PetCareContext context, ImageHelper imageHelper, PhotoTransfer photoTransfer)
        {
            _context = context;
            _imageHelper = imageHelper;
            _photoTransfer = photoTransfer;
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

                // Get Photo from MVC Application
                var photoBytes = _photoTransfer.GetPhotoAsync(pet.Photo);

                await _imageHelper.DownloadAndSaveImageAsync(pet.Photo, imagePath);

                // Update the Photo property to the new local path
                pet.Photo = $"/images/pets/{pet.Id}.jpg";
                var updateDefinition = Builders<Pet>.Update.Set(o => o.Photo, pet.Photo);
                await _context.Pets.UpdateOneAsync(o => o.Id == pet.Id, updateDefinition);
            }
            
        }

        public async Task DeleteAsync(Guid id)
        {
            var pet = await _context.Pets.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (pet != null)
            {
                // Se a foto existir, delete-a
                if (!string.IsNullOrEmpty(pet.Photo))
                {
                    string photoPath = Path.Combine("wwwroot", pet.Photo.TrimStart('/'));
                    if (System.IO.File.Exists(photoPath))
                    {
                        System.IO.File.Delete(photoPath);
                    }
                }

                // Exclui o pet do banco de dados
                await _context.Pets.DeleteOneAsync(p => p.Id == id);
            }
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {           
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
