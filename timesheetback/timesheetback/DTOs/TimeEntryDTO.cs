using System;
using Microsoft.CodeAnalysis;
using timesheetback.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace timesheetback.DTOs
{
	public class TimeEntryDTO
	{
        public long Id { get; set; }
        public string? Description { get; set; }
        public double Hours { get; set; }
        public double? Overtime { get; set; }
        public DateTime Date { get; set; }
        public long? EmployeeId { get; set; }
        public long? ClientId { get; set; }
        public long? ProjectId { get; set; }
        public long? CategoryId { get; set; }

        public TimeEntryDTO()
		{
		}

        public TimeEntryDTO(long id, string? description, double hours, double? overtime, DateTime date, long? employeeId, long? clientId, long? projectId, long? categoryId)
        {
            Id = id;
            Description = description;
            Hours = hours;
            Overtime = overtime;
            Date = date;
            EmployeeId = employeeId;
            ClientId = clientId;
            ProjectId = projectId;
            CategoryId = categoryId;
        }

        public TimeEntryDTO(TimeEntry timeEntry) {
            Id = timeEntry.Id;
            Description = timeEntry.Description;
            Hours = timeEntry.Hours;
            Overtime = timeEntry.Overtime;
            Date = timeEntry.Date;
            EmployeeId = timeEntry.EmployeeId;
            ClientId = timeEntry.ClientId;
            ProjectId = timeEntry.ProjectId;
            CategoryId = timeEntry.CategoryId;
        }
    }
}

