using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Permission
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();
        Task<PermissionDto> GetPermissionByIdAsync(Guid id);
        Task CreatePermissionAsync(PermissionDto permDto);
        Task UpdatePermissionAsync(PermissionDto permDto);
        Task DeletePermissionAsync(Guid id);
    }
}
