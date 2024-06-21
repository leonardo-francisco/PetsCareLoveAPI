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
    public class TrainingResultRepository : ITrainingResultRepository
    {
        private readonly PetCareContext _context;

        public TrainingResultRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TrainingResult trainingRes)
        {
            await _context.TrainingResults.InsertOneAsync(trainingRes);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.TrainingResults.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TrainingResult>> GetAllAsync()
        {
            return await _context.TrainingResults.Find(_ => true).ToListAsync();
        }

        public async Task<TrainingResult> GetByIdAsync(Guid id)
        {
            return await _context.TrainingResults.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TrainingResult trainingRes)
        {
            var filter = Builders<TrainingResult>.Filter.Eq(p => p.Id, trainingRes.Id);
            await _context.TrainingResults.ReplaceOneAsync(filter, trainingRes);
        }
    }
}
