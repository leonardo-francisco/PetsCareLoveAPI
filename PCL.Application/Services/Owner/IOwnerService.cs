using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Owner
{
    public interface IOwnerService
    {
        Task<IEnumerable<OwnerDto>> GetAllOwnersAsync();
        Task<OwnerDto> GetOwnerByIdAsync(Guid id);
        Task AddOwnerAsync(OwnerDto ownerDto);
        Task UpdateOwnerAsync(OwnerDto ownerDto);
        Task DeleteOwnerAsync(Guid id);
    }
}
