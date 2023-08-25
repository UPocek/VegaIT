using System;
using timesheetback.Models;

namespace timesheetback.Repositories
{
	public interface IUserRepository
	{

		List<Role> GetAllRoles();
		Task<List<Role>> GetAllRolesAsync();

		Employee? GetUserByEmail(string emial);
        Task<Employee?> GetUserByEmailAsync(string email);

		Role? GetRoleByName(string roleName);
		Task<Role?> GetRoleByNameAsync(string roleName);

		List<Employee> GetAllEmployees();
		Task<List<Employee>> GetAllEmployeesAsync();

        void SaveUser(Employee user);
    }
}

