using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class UserMinimalDTO
	{
        public long Id { get; set; }
        public string Name { get; set; }

        public UserMinimalDTO()
		{
		}

        public UserMinimalDTO(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public UserMinimalDTO(Employee user) {
            Id = user.Id;
            Name = user.Name;
        }
    }
}

