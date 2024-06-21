using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.MedicalRecord
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task AddMedicalRecordAsync(MedicalRecordDto medicalRecordDto)
        {
            var medRec = _mapper.Map<PCL.Domain.Entities.MedicalRecord>(medicalRecordDto);
            await _medicalRecordRepository.AddMedicalRecordAsync(medRec);
        }

        public async Task<MedicalRecordDto> GetMedicalRecordByAppointmentIdAsync(Guid appointmentId)
        {
            var medRec = await _medicalRecordRepository.GetMedicalRecordByAppointmentIdAsync(appointmentId);
            return _mapper.Map<MedicalRecordDto>(medRec);
        }

        public async Task<MedicalRecordDto> GetMedicalRecordByIdAsync(Guid medicalRecordId)
        {
            var medRec = await _medicalRecordRepository.GetMedicalRecordByIdAsync(medicalRecordId);
            return _mapper.Map<MedicalRecordDto>(medRec);
        }

        public async Task<MedicalRecordDto> GetMedicalRecordByPetIdAsync(Guid petId)
        {
            var medRec = await _medicalRecordRepository.GetMedicalRecordByPetIdAsync(petId);
            return _mapper.Map<MedicalRecordDto>(medRec);
        }

        public async Task UpdateMedicalRecordAsync(MedicalRecordDto medicalRecordDto)
        {
            var medRec = _mapper.Map<PCL.Domain.Entities.MedicalRecord>(medicalRecordDto);
            await _medicalRecordRepository.UpdateMedicalRecordAsync(medRec);
        }
    }
}
