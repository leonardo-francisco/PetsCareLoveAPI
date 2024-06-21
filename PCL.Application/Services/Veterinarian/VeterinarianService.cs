using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Veterinarian
{
    public class VeterinarianService : IVeterinarianService
    {
        private readonly IVeterinarianRepository _veterinarianRepository;
        private readonly IMapper _mapper;

        public VeterinarianService(IVeterinarianRepository veterinarianRepository, IMapper mapper)
        {
            _veterinarianRepository = veterinarianRepository;
            _mapper = mapper;
        }

        public async Task CreateVeterinarianAsync(VeterinarianDto vetDto)
        {
            var vet = _mapper.Map<PCL.Domain.Entities.Veterinarian>(vetDto);
            await _veterinarianRepository.CreateVeterinarianAsync(vet);
        }

        public async Task DeleteVeterinarianAsync(Guid id)
        {
            var vet = await _veterinarianRepository.GetVeterinarianByIdAsync(id);
            await _veterinarianRepository.DeleteVeterinarianAsync(vet.Id);
        }

        public async Task<IEnumerable<VeterinarianDto>> GetAllVeterinarians()
        {
            var vets = await _veterinarianRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<VeterinarianDto>>(vets);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByVeterinarianIdAsync(Guid veterinarianId)
        {
            var appoints = await _veterinarianRepository.GetAppointmentsByVeterinarianIdAsync(veterinarianId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appoints);
        }

        public async Task<IEnumerable<ExaminationResultDto>> GetExaminationResultsByVeterinarianIdAsync(Guid veterinarianId)
        {
            var examResults = await _veterinarianRepository.GetExaminationResultsByVeterinarianIdAsync(veterinarianId);
            return _mapper.Map<IEnumerable<ExaminationResultDto>>(examResults);
        }

        public async Task<MedicalRecordDto> GetMedicalRecordByPetIdAsync(Guid petId)
        {
            var record = await _veterinarianRepository.GetMedicalRecordByPetIdAsync(petId);
            return _mapper.Map<MedicalRecordDto>(record);
        }

        public async Task<IEnumerable<PetDto>> GetPetsByVeterinarianIdAsync(Guid veterinarianId)
        {
            var pets = await _veterinarianRepository.GetPetsByVeterinarianIdAsync(veterinarianId);
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<VeterinarianDto> GetVeterinarianByIdAsync(Guid veterinarianId)
        {
            var vet = await _veterinarianRepository.GetVeterinarianByIdAsync(veterinarianId);
            return _mapper.Map<VeterinarianDto>(vet);
        }

        public async Task PrescribeExaminationAsync(ExaminationDto examination)
        {
            var exam = _mapper.Map<PCL.Domain.Entities.Examination>(examination);
            await _veterinarianRepository.PrescribeExaminationAsync(exam);
        }

        public async Task UpdateVeterinarianAsync(VeterinarianDto vetDto)
        {
            var vet = _mapper.Map<PCL.Domain.Entities.Veterinarian>(vetDto);
            await _veterinarianRepository.UpdateVeterinarianAsync(vet);
        }
    }
}
