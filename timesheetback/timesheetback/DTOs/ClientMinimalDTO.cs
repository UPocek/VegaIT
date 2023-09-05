using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class ClientMinimalDTO
	{
        public long Id { get; set; }
        public string Name { get; set; }

        public ClientMinimalDTO()
		{
		}

        public ClientMinimalDTO(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public ClientMinimalDTO(Client client) {
            Id = client.Id;
            Name = client.Name;
        }
    }
}

