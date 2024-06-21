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
    public class ExaminationRepository : IExaminationRepository
    {
        private readonly PetCareContext _context;

        public ExaminationRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task<Examination> GetExaminationByIdAsync(Guid examinationId)
        {
            return await _context.Examinations.Find(g => g.Id == examinationId).FirstOrDefaultAsync();
        }

        //public async Task<IEnumerable<ExaminationResult>> GetExaminationResultsByVeterinarianIdAsync(Guid veterinarianId)
        //{
        //    return await _context.ExaminationResults.Find(g => g. == veterinarianId).FirstOrDefaultAsync();
        //}

        public async Task<IEnumerable<Examination>> GetExaminationsByPetIdAsync(Guid petId)
        {
            return await _context.Examinations.Find(g => g.PetId == petId).ToListAsync();
        }

        //public async Task<IEnumerable<Examination>> GetExaminationsByVeterinarianIdAsync(Guid veterinarianId)
        //{
        //    return await _context.Examinations.Find(g => g. == petId).ToListAsync();
        //}

        public async Task PrescribeExaminationAsync(Examination examination)
        {
            await _context.Examinations.InsertOneAsync(examination);
        }

        public async Task UpdateExaminationAsync(Examination examination)
        {
            var filter = Builders<Examination>.Filter.Eq(p => p.Id, examination.Id);
            await _context.Examinations.ReplaceOneAsync(filter, examination);
        }

        public async Task UpdateExaminationResultAsync(ExaminationResult examinationResult)
        {
            var filter = Builders<ExaminationResult>.Filter.Eq(p => p.Id, examinationResult.Id);
            await _context.ExaminationResults.ReplaceOneAsync(filter, examinationResult);
        }
    }
}
