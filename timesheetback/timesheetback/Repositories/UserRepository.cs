using System;
using timesheetback.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace timesheetback.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly TimeSheetContext _context;

        public UserRepository(TimeSheetContext context)
		{
			_context = context;
		}

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public Task<List<Employee>> GetAllEmployeesAsync()
        {
            return _context.Employees.ToListAsync();
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public Task<List<Role>> GetAllRolesAsync()
        {
            return _context.Roles.ToListAsync();
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
    }
}

