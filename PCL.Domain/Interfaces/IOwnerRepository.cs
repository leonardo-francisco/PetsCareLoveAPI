using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface IOwnerRepository
    {     
        Task<IEnumerable<Owner>> GetAllAsync();
        Task<Owner> GetByIdAsync(Guid id);
        Task AddAsync(Owner owner);
        Task UpdateAsync(Owner owner);
        Task DeleteAsync(Guid id);
      
        Task<IEnumerable<Pet>> GetPetsByOwnerIdAsync(Guid ownerId);
        Task<IEnumerable<Appointment>> GetAppointmentsByOwnerIdAsync(Guid ownerId);
        Task<IEnumerable<Service>> GetExamsByPetIdAsync(Guid petId);
    }
}
