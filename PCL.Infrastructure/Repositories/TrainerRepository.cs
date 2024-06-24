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
    public class TrainerRepository : ITrainerRepository
    {
        private readonly PetCareContext _context;
        private readonly ImageHelper _imageHelper;

        public TrainerRepository(PetCareContext context, ImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;

        }

        public async Task CreateAsync(Trainer trainer)
        {
            await _context.Trainers.InsertOneAsync(trainer);

            if (!string.IsNullOrEmpty(trainer.Photo))
            {
                string directoryPath = Path.Combine("wwwroot", "images", "trainer");
                Directory.CreateDirectory(directoryPath);

                string imagePath = Path.Combine(directoryPath, $"{trainer.Id}.jpg");

                await _imageHelper.DownloadAndSaveImageAsync(trainer.Photo, imagePath);

                // Update the Photo property to the new local path
                trainer.Photo = $"/images/trainer/{trainer.Id}.jpg";
                var updateDefinition = Builders<Trainer>.Update.Set(o => o.Photo, trainer.Photo);
                await _context.Trainers.UpdateOneAsync(o => o.Id == trainer.Id, updateDefinition);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var trng = await _context.Trainers.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (trng != null)
            {
                // Se a foto existir, delete-a
                if (!string.IsNullOrEmpty(trng.Photo))
                {
                    string photoPath = Path.Combine("wwwroot", trng.Photo.TrimStart('/'));
                    if (System.IO.File.Exists(photoPath))
                    {
                        System.IO.File.Delete(photoPath);
                    }
                }

                // Exclui o pet do banco de dados
                await _context.Trainers.DeleteOneAsync(p => p.Id == id);
            }           
        }

        public async Task<IEnumerable<Trainer>> GetAllAsync()
        {
            return await _context.Trainers.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByTrainerIdAsync(Guid trainerId)
        {
            return await _context.Appointments.Find(g => g.TrainerId == trainerId).ToListAsync();
        }

        public async Task<Trainer> GetByIdAsync(Guid id)
        {
            return await _context.Trainers.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pet>> GetPetsByTrainerIdAsync(Guid trainerId)
        {
            var appointments = await _context.Appointments
                .Find(a => a.TrainerId == trainerId)
                .ToListAsync();

            // Extrai os IDs dos pets dos agendamentos
            var petIds = appointments.Select(a => a.PetId);

            // Consulta os pets com base nos IDs obtidos
            var pets = await _context.Pets
                .Find(p => petIds.Contains(p.Id))
                .ToListAsync();

            return pets;
        }

        public async Task<TrainingRecord> GetTrainingRecordByPetIdAsync(Guid petId)
        {
            return await _context.TrainingRecords.Find(g => g.PetId == petId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TrainingResult>> GetTrainingResultsByTrainerIdAsync(Guid trainerId)
        {
            var trainingResults = await _context.TrainingResults
                                        .Aggregate()
                                        .Lookup<TrainingResult, Training, TrainingResult>(
                                            _context.Trainings,
                                            tr => tr.TrainingId,
                                            t => t.Id,
                                            tr => tr)
                                        .Lookup<TrainingResult, Pet, TrainingResult>(
                                            _context.Pets,
                                            tr => tr.PetId,
                                            p => p.Id,
                                            tr => tr)
                                        .Match(tr => tr.TrainerId == trainerId)
                                        .ToListAsync();

            return trainingResults;
        }

        public async Task PrescribeTrainingAsync(Training training)
        {
            await _context.Trainings.InsertOneAsync(training);
        }

        public async Task UpdateAsync(Trainer trainer)
        {
            var filter = Builders<Trainer>.Filter.Eq(p => p.Id, trainer.Id);
            await _context.Trainers.ReplaceOneAsync(filter, trainer);
        }
    }
}
