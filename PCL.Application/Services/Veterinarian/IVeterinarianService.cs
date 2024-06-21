using PCL.Application.Dto;
using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Veterinarian
{
    public interface IVeterinarianService
    {
        Task<IEnumerable<VeterinarianDto>> GetAllVeterinarians();
        Task<VeterinarianDto> GetVeterinarianByIdAsync(Guid veterinarianId);
        Task CreateVeterinarianAsync(VeterinarianDto vetDto);
        Task UpdateVeterinarianAsync(VeterinarianDto vetDto);
        Task DeleteVeterinarianAsync(Guid id);

        Task<IEnumerable<AppointmentDto>> GetAppointmentsByVeterinarianIdAsync(Guid veterinarianId);

        Task<IEnumerable<PetDto>> GetPetsByVeterinarianIdAsync(Guid veterinarianId);

        Task PrescribeExaminationAsync(ExaminationDto examination);

        Task<IEnumerable<ExaminationResultDto>> GetExaminationResultsByVeterinarianIdAsync(Guid veterinarianId);

        Task<MedicalRecordDto> GetMedicalRecordByPetIdAsync(Guid petId);
    }
}
