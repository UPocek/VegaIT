using System;
using timesheetback.DTOs;
using timesheetback.Models;
using timesheetback.Repositories;

namespace timesheetback.Services
{
	public class RoleService : IRoleService
	{
        private readonly IRoleRepository _roleRepository;
		public RoleService(IRoleRepository roleRepository)
		{
            _roleRepository = roleRepository;
		}

        public List<RoleDTO> GetAllRoles()
        {
            List<Role> allRoles = _roleRepository.GetAllRoles();
            return allRoles.Select(role => new RoleDTO(role)).ToList();
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            List<Role> allRoles = await _roleRepository.GetAllRolesAsync();
            return allRoles.Select(role => new RoleDTO(role)).ToList();
        }
    }
}

