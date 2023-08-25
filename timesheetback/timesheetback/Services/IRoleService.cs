using System;
using timesheetback.DTOs;

namespace timesheetback.Services
{
	public interface IRoleService
	{
        List<RoleDTO> GetAllRoles();
        Task<List<RoleDTO>> GetAllRolesAsync();
    }
}

