using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Appointment
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> GetAppointmentByIdAsync(Guid appointmentId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByVeterinarianIdAsync(Guid veterinarianId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByOwnerIdAsync(Guid ownerId);
        Task ScheduleAppointmentAsync(AppointmentDto appointmentDto);
        Task UpdateAppointmentAsync(AppointmentDto appointmentDto);
        Task CancelAppointmentAsync(Guid appointmentId);
    }
}
