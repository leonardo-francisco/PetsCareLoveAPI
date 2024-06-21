using AutoMapper;
using PCL.Application.Dto;
using PCL.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionService(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task CreatePermissionAsync(PermissionDto permDto)
        {
            var perm = _mapper.Map<PCL.Domain.Entities.Permission>(permDto);
            await _permissionRepository.CreateAsync(perm);
        }

        public async Task DeletePermissionAsync(Guid id)
        {
            var perm = await _permissionRepository.GetByIdAsync(id);
            await _permissionRepository.DeleteAsync(perm.Id);
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
        {
            var perms = await _permissionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PermissionDto>>(perms);
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(Guid id)
        {
            var perm = await _permissionRepository.GetByIdAsync(id);
            return _mapper.Map<PermissionDto>(perm);
        }

        public async Task UpdatePermissionAsync(PermissionDto permDto)
        {
            var perm = _mapper.Map<PCL.Domain.Entities.Permission>(permDto);
            await _permissionRepository.UpdateAsync(perm);
        }
    }
}
