using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class NewTimeEntryDTO
	{
        public string? Description { get; set; }
        public double Hours { get; set; }
        public double? Overtime { get; set; }
        public string Date { get; set; }
        public long? ClientId { get; set; }
        public long? ProjectId { get; set; }
        public long? CategoryId { get; set; }

        public NewTimeEntryDTO()
		{
		}

        public NewTimeEntryDTO(string? description, double hours, double? overtime, string date, long? clientId, long? projectId, long? categoryId)
        {
            Description = description;
            Hours = hours;
            Overtime = overtime;
            Date = date;
            ClientId = clientId;
            ProjectId = projectId;
            CategoryId = categoryId;
        }
    }
}

