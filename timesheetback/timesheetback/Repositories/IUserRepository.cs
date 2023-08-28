using System;
using timesheetback.DTOs;
using timesheetback.Models;

namespace timesheetback.Repositories
{
	public interface IUserRepository
	{

		Employee? GetUserByEmail(string emial);
        Task<Employee?> GetUserByEmailAsync(string email);

		Role? GetRoleByName(string roleName);
		Task<Role?> GetRoleByNameAsync(string roleName);

		List<Employee> GetAllEmployees();
		Task<List<Employee>> GetAllEmployeesAsync();

		Employee? GetEmployeeById(long id);
        Task<Employee?> GetEmployeeByIdAsync(long id);

		void DeleteEmployee(long id);
		Task DeleteEmployeeAsync(long id);

        Employee UpdateEmployee(Employee employeeToUpdate, RegistrationCredentialsDTO registrationCredentials);
        Employee SaveUser(Employee user);
    }
}

