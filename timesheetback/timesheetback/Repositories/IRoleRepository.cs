using System;
using timesheetback.Models;

namespace timesheetback.Repositories
{
	public interface IRoleRepository
	{
        List<Role> GetAllRoles();
        Task<List<Role>> GetAllRolesAsync();
    }
}

