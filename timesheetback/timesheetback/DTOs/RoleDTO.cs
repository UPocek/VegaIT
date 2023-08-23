using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class RoleDTO
	{
        public long Id { get; set; }
        public string Name { get; set; }

        public RoleDTO()
		{
		}

        public RoleDTO(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public RoleDTO(Role role) {
            Id = role.Id;
            Name = role.Name;
        }
    }
}

