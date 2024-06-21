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
    public class TrainingRecordRepository : ITrainingRecordRepository
    {
        private readonly PetCareContext _context;

        public TrainingRecordRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TrainingRecord trainingRec)
        {
            await _context.TrainingRecords.InsertOneAsync(trainingRec);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.TrainingRecords.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TrainingRecord>> GetAllAsync()
        {
            return await _context.TrainingRecords.Find(_ => true).ToListAsync();
        }

        public async Task<TrainingRecord> GetByIdAsync(Guid id)
        {
            return await _context.TrainingRecords.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TrainingRecord trainingRec)
        {
            var filter = Builders<TrainingRecord>.Filter.Eq(p => p.Id, trainingRec.Id);
            await _context.TrainingRecords.ReplaceOneAsync(filter, trainingRec);
        }
    }
}
