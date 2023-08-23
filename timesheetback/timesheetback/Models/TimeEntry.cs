using System;
namespace timesheetback.Models
{
	public class TimeEntry
	{
		public long Id { get; set; }
		public string? Description { get; set; }
        public double Hours { get; set; }
        public double? Overtime { get; set; }
        public DateTime Date { get; set; }
		public long EmployeeId { get; set; }
		public Employee Employee { get; set; } = null!;
		public long ClientId { get; set; }
		public Client Client { get; set; } = null!;
        public long ProjectId { get; set; }
		public Project Project { get; set; } = null!;
		public long CategoryId { get; set; }
		public Category Category { get; set; } = null!;

        public TimeEntry()
		{
		}

        public TimeEntry(long id, string? description, double hours, double? overtime, DateTime date, long employeeId, Employee employee, long clientId, Client client, long projectId, Project project, long categoryId, Category category)
        {
            Id = id;
            Description = description;
            Hours = hours;
            Overtime = overtime;
            Date = date;
            EmployeeId = employeeId;
            Employee = employee;
            ClientId = clientId;
            Client = client;
            ProjectId = projectId;
            Project = project;
            CategoryId = categoryId;
            Category = category;
        }
    }
}

