using System;
using timesheetback.DTOs;
using timesheetback.Models;

namespace timesheetback.Repositories
{
	public interface ITimeEntryRepository
	{
		List<TotalTimeDTO> GetTotalTimeEntriesForDateRange(string userEmail, string startDate, string endDate);
        Task<List<TotalTimeDTO>> GetTotalTimeEntriesForDateRangeAsync(string userEmail, string startDate, string endDate);

		TimeEntry? GetEntryById(long id);
		Task<TimeEntry?> GetEntryByIdAsync(long id);

        TimeEntry SaveEntry(Employee user, TimeEntry newTimeEntry);
		TimeEntry UpdateEntry(TimeEntry timeEntryToUpdate, NewTimeEntryDTO newTimeEntry);

		List<TimeEntry> GetAllUserEntriesForDate(string userEmail, string date);
		Task<List<TimeEntry>> GetAllUserEntriesForDateAsync(string userEmail, string date);

		List<TimeEntryDetailedDTO> GetTimeEntriesReport(long client, long project, long category, long employee, string time);
		Task<List<TimeEntryDetailedDTO>> GetTimeEntriesReportAsync(long client, long project, long category, long employee, string time);
    }
}

