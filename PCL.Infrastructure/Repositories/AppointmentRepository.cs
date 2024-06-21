using MongoDB.Driver;
using PCL.Domain.Entities;
using PCL.Domain.Enums;
using PCL.Domain.Interfaces;
using PCL.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly PetCareContext _context;

        public AppointmentRepository(PetCareContext context)
        {
            _context = context;
        }

        public async Task CancelAppointmentAsync(Guid appointmentId)
        {
            var filter = Builders<Appointment>.Filter.Eq(a => a.Id, appointmentId);
            var update = Builders<Appointment>.Update
                .Set(a => a.AppointmentStatus, AppointmentStatus.Canceled)
                .Set(a => a.Notes, "Consulta cancelada");

            var result = await _context.Appointments.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                throw new Exception("Appointment not found.");
            }

            if (result.ModifiedCount == 0)
            {
                throw new Exception("Appointment not updated.");
            }
        }

        public async Task<Appointment> GetAppointmentByIdAsync(Guid appointmentId)
        {
            return await _context.Appointments.Find(g => g.Id == appointmentId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByOwnerIdAsync(Guid ownerId)
        {
            return await _context.Appointments.Find(g => g.OwnerId == ownerId).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByVeterinarianIdAsync(Guid veterinarianId)
        {
            return await _context.Appointments.Find(g => g.VeterinarianId == veterinarianId).ToListAsync();
        }

        public async Task ScheduleAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.InsertOneAsync(appointment);
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            var filter = Builders<Appointment>.Filter.Eq(p => p.Id, appointment.Id);
            await _context.Appointments.ReplaceOneAsync(filter, appointment);
        }
    }
}
