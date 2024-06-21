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
    public class VeterinarianRepository : IVeterinarianRepository
    {
        private readonly PetCareContext _context;
        private readonly ImageHelper _imageHelper;

        public VeterinarianRepository(PetCareContext context, ImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
        }

        public async Task CreateVeterinarianAsync(Veterinarian veterinarian)
        {
            await _context.Veterinarians.InsertOneAsync(veterinarian);

            if (!string.IsNullOrEmpty(veterinarian.Photo))
            {
                string directoryPath = Path.Combine("wwwroot", "images", "veterinarian");
                Directory.CreateDirectory(directoryPath);

                string imagePath = Path.Combine(directoryPath, $"{veterinarian.Id}.jpg");

                await _imageHelper.DownloadAndSaveImageAsync(veterinarian.Photo, imagePath);

                // Update the Photo property to the new local path
                veterinarian.Photo = $"/images/veterinarian/{veterinarian.Id}.jpg";
                var updateDefinition = Builders<Veterinarian>.Update.Set(o => o.Photo, veterinarian.Photo);
                await _context.Veterinarians.UpdateOneAsync(o => o.Id == veterinarian.Id, updateDefinition);
            }
        }

        public async Task DeleteVeterinarianAsync(Guid id)
        {
            await _context.Veterinarians.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Veterinarian>> GetAllAsync()
        {
            var vets = await _context.Veterinarians.Find(_ => true).ToListAsync();
            return vets;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByVeterinarianIdAsync(Guid veterinarianId)
        {
            var appointments = await _context.Appointments
                .Find(a => a.VeterinarianId == veterinarianId)
                .ToListAsync();

            return appointments;
        }

        public Task<IEnumerable<ExaminationResult>> GetExaminationResultsByVeterinarianIdAsync(Guid veterinarianId)
        {
            throw new NotImplementedException();
        }

        public async Task<MedicalRecord> GetMedicalRecordByPetIdAsync(Guid petId)
        {
            return await _context.MedicalRecords.Find(mr => mr.PetId == petId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pet>> GetPetsByVeterinarianIdAsync(Guid veterinarianId)
        {
            var appointments = await _context.Appointments
                .Find(a => a.VeterinarianId == veterinarianId)
                .ToListAsync();

            // Extrai os IDs dos pets dos agendamentos
            var petIds = appointments.Select(a => a.PetId);

            // Consulta os pets com base nos IDs obtidos
            var pets = await _context.Pets
                .Find(p => petIds.Contains(p.Id))
                .ToListAsync();

            return pets;
        }

        public async Task<Veterinarian> GetVeterinarianByIdAsync(Guid veterinarianId)
        {
            var filter = Builders<Veterinarian>.Filter.Eq(p => p.Id, veterinarianId);
            var vet = await _context.Veterinarians.Find(filter).FirstOrDefaultAsync();

            if (vet != null)
            {
                var apoint = await _context.Appointments.Find(f => f.VeterinarianId == vet.Id).ToListAsync();
                vet.Appointments = apoint;
            }

            return vet;
        }

        public async Task PrescribeExaminationAsync(Examination examination)
        {
            await _context.Examinations.InsertOneAsync(examination);
        }

        public async Task UpdateVeterinarianAsync(Veterinarian veterinarian)
        {
            var existingVet = await _context.Veterinarians.Find(o => o.Id == veterinarian.Id).FirstOrDefaultAsync();

            if (existingVet == null)
            {
                throw new Exception("Veterinarian not found");
            }

            bool photoChanged = existingVet.Photo != veterinarian.Photo;

            if (photoChanged && !string.IsNullOrEmpty(veterinarian.Photo))
            {
                string directoryPath = Path.Combine("wwwroot", "images", "veterinarian");
                Directory.CreateDirectory(directoryPath);

                string imagePath = Path.Combine(directoryPath, $"{veterinarian.Id}.jpg");

                await _imageHelper.DownloadAndSaveImageAsync(veterinarian.Photo, imagePath);

                // Update the Photo property to the new local path
                veterinarian.Photo = $"/images/veterinarian/{veterinarian.Id}.jpg";
            }

            var updateDefinition = Builders<Veterinarian>.Update
                .Set(o => o.Name, veterinarian.Name)
                .Set(o => o.Crmv, veterinarian.Crmv)
                .Set(o => o.Email, veterinarian.Email)
                .Set(o => o.Phone, veterinarian.Phone);              

            if (photoChanged)
            {
                updateDefinition = updateDefinition.Set(o => o.Photo, veterinarian.Photo);
            }

            await _context.Veterinarians.UpdateOneAsync(o => o.Id == veterinarian.Id, updateDefinition);
        }
    }
}
