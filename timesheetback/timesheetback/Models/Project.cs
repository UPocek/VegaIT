using System;
namespace timesheetback.Models
{
	public class Project
	{

		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
		public long ClientId { get; set; }
		public Client Client { get; set; } = null!;
		public long EmployeeId { get; set; }
		public Employee Employee { get; set; }

        public Project()
		{
		}

        public Project(long id, string name, string description, string status, long clientId, Client client, long employeeId, Employee employee)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            ClientId = clientId;
            Client = client;
            EmployeeId = employeeId;
            Employee = employee;
        }
    }
}

