using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class UserDTO
	{
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public long RoleId { get; set; }

        public UserDTO()
		{
		}

        public UserDTO(long id, string name, string username, string email, bool isActive, long roleId)
        {
            Id = id;
            Name = name;
            Username = username;
            Email = email;
            IsActive = isActive;
            RoleId = roleId;
        }

        public UserDTO(Employee user) {
            Id = user.Id;
            Name = user.Name;
            Username = user.Username;
            Email = user.Email;
            IsActive = user.IsActive;
            RoleId = user.RoleId;
        }
    }
}

