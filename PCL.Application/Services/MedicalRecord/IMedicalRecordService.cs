using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.MedicalRecord
{
    public interface IMedicalRecordService
    {
        Task<MedicalRecordDto> GetMedicalRecordByIdAsync(Guid medicalRecordId);
        Task<MedicalRecordDto> GetMedicalRecordByPetIdAsync(Guid petId);
        Task<MedicalRecordDto> GetMedicalRecordByAppointmentIdAsync(Guid appointmentId);
        Task AddMedicalRecordAsync(MedicalRecordDto medicalRecordDto);
        Task UpdateMedicalRecordAsync(MedicalRecordDto medicalRecordDto);
    }
}
