using System;
namespace timesheetback.DTOs
{
	public class TimeEntryDTODetailed
	{
        public long Id { get; set; }
        public string? Description { get; set; }
        public double Hours { get; set; }
        public double? Overtime { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeName { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public string CategoryName { get; set; }

        public TimeEntryDTODetailed()
		{

		}

        public TimeEntryDTODetailed(long id, string? description, double hours, double? overtime, DateTime date, string employeeName, string clientName, string projectName, string categoryName)
        {
            Id = id;
            Description = description;
            Hours = hours;
            Overtime = overtime;
            Date = date;
            EmployeeName = employeeName;
            ClientName = clientName;
            ProjectName = projectName;
            CategoryName = categoryName;
        }
    }
}

