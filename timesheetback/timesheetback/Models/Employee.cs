using System;
using Microsoft.Extensions.Hosting;
using timesheetback.DTOs;

namespace timesheetback.Models
{
	public class Employee
	{
        public long Id { get; set; }
		public string Name { get; set; }
		public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public ICollection<TimeEntry> TimeEntrys { get; } = new List<TimeEntry>();

        public Employee()
		{
		}

        public Employee(long id, string name, string username, string email, string password, bool isActive, long roleId, Role role, ICollection<TimeEntry> timeEntrys)
        {
            Id = id;
            Name = name;
            Username = username;
            Email = email;
            Password = password;
            IsActive = isActive;
            RoleId = roleId;
            Role = role;
            TimeEntrys = timeEntrys;
        }

        public Employee(RegistrationCredentialsDTO registrationCredentials, Role role) {
            Name = registrationCredentials.Name;
            Username = registrationCredentials.Username;
            Email = registrationCredentials.Email;
            Password = registrationCredentials.Password;
            IsActive = (bool)(registrationCredentials.Status == null ? true : registrationCredentials.Status);
            Role = role;
            TimeEntrys = new List<TimeEntry>();
        }
    }
}

