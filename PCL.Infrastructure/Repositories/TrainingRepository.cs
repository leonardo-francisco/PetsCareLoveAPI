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
    public class TrainingRepository : ITrainingRepository
    {
        private readonly PetCareContext _context;

        public TrainingRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Training training)
        {
            await _context.Trainings.InsertOneAsync(training);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Trainings.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Training>> GetAllAsync()
        {
            return await _context.Trainings.Find(_ => true).ToListAsync();
        }

        public async Task<Training> GetByIdAsync(Guid id)
        {
            return await _context.Trainings.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Training training)
        {
            var filter = Builders<Training>.Filter.Eq(p => p.Id, training.Id);
            await _context.Trainings.ReplaceOneAsync(filter, training);
        }
    }
}
