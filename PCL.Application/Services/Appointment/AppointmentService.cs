using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Entities;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task CancelAppointmentAsync(Guid appointmentId)
        {         
            await _appointmentRepository.CancelAppointmentAsync(appointmentId);
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(Guid appointmentId)
        {
            var appt = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
            return _mapper.Map<AppointmentDto>(appt);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByOwnerIdAsync(Guid ownerId)
        {
            var appts = await _appointmentRepository.GetAppointmentsByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appts);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByVeterinarianIdAsync(Guid veterinarianId)
        {
            var appts = await _appointmentRepository.GetAppointmentsByVeterinarianIdAsync(veterinarianId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appts);
        }

        public async Task ScheduleAppointmentAsync(AppointmentDto appointmentDto)
        {
            var appt = _mapper.Map<PCL.Domain.Entities.Appointment>(appointmentDto);
            await _appointmentRepository.ScheduleAppointmentAsync(appt);
        }

        public async Task UpdateAppointmentAsync(AppointmentDto appointmentDto)
        {
            var appt = _mapper.Map<PCL.Domain.Entities.Appointment>(appointmentDto);
            await _appointmentRepository.UpdateAppointmentAsync(appt);
        }
    }
}
