using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<MedicalRecord> GetMedicalRecordByIdAsync(Guid medicalRecordId);
        Task<MedicalRecord> GetMedicalRecordByPetIdAsync(Guid petId);
        Task<MedicalRecord> GetMedicalRecordByAppointmentIdAsync(Guid appointmentId);
        Task AddMedicalRecordAsync(MedicalRecord medicalRecord);
        Task UpdateMedicalRecordAsync(MedicalRecord medicalRecord);
    }
}
