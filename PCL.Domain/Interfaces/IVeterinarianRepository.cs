using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface IVeterinarianRepository
    {
        Task<IEnumerable<Veterinarian>> GetAllAsync();
        Task<Veterinarian> GetVeterinarianByIdAsync(Guid veterinarianId);
        Task CreateVeterinarianAsync(Veterinarian veterinarian);
        Task UpdateVeterinarianAsync(Veterinarian veterinarian);
        Task DeleteVeterinarianAsync(Guid id);
        Task<IEnumerable<Appointment>> GetAppointmentsByVeterinarianIdAsync(Guid veterinarianId);

        Task<IEnumerable<Pet>> GetPetsByVeterinarianIdAsync(Guid veterinarianId);

        Task PrescribeExaminationAsync(Examination examination);

        Task<IEnumerable<ExaminationResult>> GetExaminationResultsByVeterinarianIdAsync(Guid veterinarianId);

        Task<MedicalRecord> GetMedicalRecordByPetIdAsync(Guid petId);
    }
}
