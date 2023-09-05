using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class ProjectDTO
	{

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public long? ClientId { get; set; }
        public long? EmployeeId { get; set; }

        public ProjectDTO()
		{
		}

        public ProjectDTO(long id, string name, string description, string status, long clientId, long employeeId)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            ClientId = clientId;
            EmployeeId = employeeId;
        }

        public ProjectDTO(Project project) {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            Status = project.Status;
            ClientId = project.ClientId;
            EmployeeId = project.EmployeeId;
        }
    }
}

