using System;
using Microsoft.EntityFrameworkCore;
using timesheetback.Models;
namespace timesheetback.Repositories
{
	public class RoleRepository : IRoleRepository
	{

		private readonly TimeSheetContext _context;
        public RoleRepository(TimeSheetContext context)
		{
			_context = context;
		}

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public Task<List<Role>> GetAllRolesAsync()
        {
            return _context.Roles.ToListAsync();
        }
    }
}

