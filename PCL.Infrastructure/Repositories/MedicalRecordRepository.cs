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
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly PetCareContext _context;

        public MedicalRecordRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task AddMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            await _context.MedicalRecords.InsertOneAsync(medicalRecord);
        }

        public async Task<MedicalRecord> GetMedicalRecordByAppointmentIdAsync(Guid appointmentId)
        {
            return await _context.MedicalRecords.Find(g => g.AppointmentId == appointmentId).FirstOrDefaultAsync();
        }

        public async Task<MedicalRecord> GetMedicalRecordByIdAsync(Guid medicalRecordId)
        {
            return await _context.MedicalRecords.Find(g => g.Id == medicalRecordId).FirstOrDefaultAsync();
        }

        public async Task<MedicalRecord> GetMedicalRecordByPetIdAsync(Guid petId)
        {
            return await _context.MedicalRecords.Find(g => g.PetId == petId).FirstOrDefaultAsync();
        }

        public async Task UpdateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            var filter = Builders<MedicalRecord>.Filter.Eq(p => p.Id, medicalRecord.Id);
            await _context.MedicalRecords.ReplaceOneAsync(filter, medicalRecord);
        }
    }
}
