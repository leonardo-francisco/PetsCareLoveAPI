using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetAppointmentByIdAsync(Guid appointmentId);
        Task<IEnumerable<Appointment>> GetAppointmentsByVeterinarianIdAsync(Guid veterinarianId);
        Task<IEnumerable<Appointment>> GetAppointmentsByOwnerIdAsync(Guid ownerId);
        Task ScheduleAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task CancelAppointmentAsync(Guid appointmentId);
    }
}
