using System;
namespace timesheetback.DTOs
{
	public class CreateProjectCredentialsDTO
	{

        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public long ClientId { get; set; }
        public long EmployeeId { get; set; }

        public CreateProjectCredentialsDTO()
		{
		}

        public CreateProjectCredentialsDTO(string name, string description, string status, long clientId, long employeeId)
        {
            Name = name;
            Description = description;
            Status = status;
            ClientId = clientId;
            EmployeeId = employeeId;
        }
    }
}

