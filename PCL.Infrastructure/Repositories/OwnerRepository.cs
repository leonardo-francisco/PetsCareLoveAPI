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
    public class OwnerRepository : IOwnerRepository
    {
        private readonly PetCareContext _context;
        private readonly ImageHelper _imageHelper;

        public OwnerRepository(PetCareContext context, ImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
        }

        public async Task AddAsync(Owner owner)
        {           
            await _context.Owners.InsertOneAsync(owner);

            if (!string.IsNullOrEmpty(owner.Photo))
            {
                string directoryPath = Path.Combine("wwwroot", "images", "owner");
                Directory.CreateDirectory(directoryPath);

                string imagePath = Path.Combine(directoryPath, $"{owner.Id}.jpg");

                await _imageHelper.DownloadAndSaveImageAsync(owner.Photo, imagePath);

                // Update the Photo property to the new local path
                owner.Photo = $"/images/owner/{owner.Id}.jpg";
                var updateDefinition = Builders<Owner>.Update.Set(o => o.Photo, owner.Photo);
                await _context.Owners.UpdateOneAsync(o => o.Id == owner.Id, updateDefinition);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Owners.DeleteOneAsync(owner => owner.Id == id);
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _context.Owners.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByOwnerIdAsync(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Owner> GetByIdAsync(Guid id)
        {
            return await _context.Owners.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Service>> GetExamsByPetIdAsync(Guid petId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Pet>> GetPetsByOwnerIdAsync(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Owner owner)
        {
            //var filter = Builders<Owner>.Filter.Eq(p => p.Id, owner.Id);
            //await _context.Owners.ReplaceOneAsync(filter,owner);
            var existingOwner = await _context.Owners.Find(o => o.Id == owner.Id).FirstOrDefaultAsync();

            if (existingOwner == null)
            {
                throw new Exception("Owner not found");
            }

            bool photoChanged = existingOwner.Photo != owner.Photo;

            if (photoChanged && !string.IsNullOrEmpty(owner.Photo))
            {
                string directoryPath = Path.Combine("wwwroot", "images", "owner");
                Directory.CreateDirectory(directoryPath); // Ensure the directory exists

                string imagePath = Path.Combine(directoryPath, $"{owner.Id}.jpg");

                await _imageHelper.DownloadAndSaveImageAsync(owner.Photo, imagePath);

                // Update the Photo property to the new local path
                owner.Photo = $"/images/owner/{owner.Id}.jpg";
            }

            var updateDefinition = Builders<Owner>.Update
                .Set(o => o.Name, owner.Name)
                .Set(o => o.Email, owner.Email)
                .Set(o => o.Phone, owner.Phone)
                .Set(o => o.PetIds, owner.PetIds);

            if (existingOwner.PetIds != null && existingOwner.PetIds.Any())
            {
                updateDefinition = updateDefinition.Set(o => o.PetIds, existingOwner.PetIds);
            }

            if (photoChanged)
            {
                updateDefinition = updateDefinition.Set(o => o.Photo, owner.Photo);
            }

            await _context.Owners.UpdateOneAsync(o => o.Id == owner.Id, updateDefinition);
        }
    }
}
