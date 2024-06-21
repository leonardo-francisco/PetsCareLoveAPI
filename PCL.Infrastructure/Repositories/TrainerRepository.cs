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
    public class TrainerRepository : ITrainerRepository
    {
        private readonly PetCareContext _context;

        public TrainerRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Trainer trainer)
        {
            await _context.Trainers.InsertOneAsync(trainer);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Trainers.DeleteOneAsync(g => g.Id == id);
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
            var trainer = await _context.Trainers.Find(t => t.Id == trainerId).FirstOrDefaultAsync();

            if (trainer == null)
            {               
                return new List<Pet>();
            }

            var appointmentIds = trainer.AppointmentIds;

            var pets = await _context.Pets.Find(p => appointmentIds.Contains(p.Id)).ToListAsync();

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
