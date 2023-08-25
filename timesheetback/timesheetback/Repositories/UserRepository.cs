using System;
using timesheetback.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using timesheetback.DTOs;

namespace timesheetback.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly TimeSheetContext _context;

        public UserRepository(TimeSheetContext context)
		{
			_context = context;
		}

        public void DeleteEmployee(long id)
        {
            var employeeToDelete = _context.Employees.Find(id) ?? throw new Exception("Employee with that id does not exist");
            _context.Employees.Remove(employeeToDelete);
            _context.SaveChanges();
        }

        public async Task DeleteEmployeeAsync(long id)
        {
            var employeeToDelete = await _context.Employees.FirstOrDefaultAsync(employee => employee.Id == id) ?? throw new Exception("Employee with that id does not exist");
            _context.Employees.Remove(employeeToDelete);
            _context.SaveChanges();
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public Task<List<Employee>> GetAllEmployeesAsync()
        {
            return _context.Employees.ToListAsync();
        }

        public Employee? GetEmployeeById(long id)
        {
            return _context.Employees.Find(id);
        }

        public Task<Employee?> GetEmployeeByIdAsync(long id)
        {
            return _context.Employees.FirstOrDefaultAsync(employee => employee.Id == id);
        }

        public Role? GetRoleByName(string roleName)
        {
            return _context.Roles.FirstOrDefault(role => role.Name == roleName);
        }

        public Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return _context.Roles.FirstOrDefaultAsync(role => role.Name == roleName);
        }

        public Employee? GetUserByEmail(string email)
        {
            return _context.Employees.Include(e => e.Role).FirstOrDefault(employee => employee.Email == email);
        }

        public Task<Employee?> GetUserByEmailAsync(string email)
        {
            return _context.Employees.Include(e => e.Role).FirstOrDefaultAsync(employee => employee.Email == email);
        }

        public void SaveUser(Employee user)
        {
            _context.Employees.Add(user);
            _context.SaveChanges();
        }

        public Employee UpdateEmployee(Employee employeeToUpdate, RegistrationCredentialsDTO registrationCredentials)
        {
            employeeToUpdate.Name = registrationCredentials.Name;
            employeeToUpdate.Username = registrationCredentials.Username;
            employeeToUpdate.Email = registrationCredentials.Email;
            employeeToUpdate.IsActive = (bool)(registrationCredentials.Status == null ? true : registrationCredentials.Status);
            employeeToUpdate.Role = GetRoleByName(registrationCredentials.Role) ?? throw new Exception("Invalid role passed");

            _context.SaveChanges();

            return employeeToUpdate;
        }
    }
}

