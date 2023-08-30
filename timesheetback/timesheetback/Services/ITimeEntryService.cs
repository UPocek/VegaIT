using System;
using timesheetback.DTOs;

namespace timesheetback.Services
{
	public interface ITimeEntryService
	{
		List<TotalTimeDTO> GetTotalTimesForDateRange(string start, string end, string token);
        Task<List<TotalTimeDTO>> GetTotalTimesForDateRangeAsync(string start, string end, string token);

		TimeEntryDTO AddNewTimeEntry(NewTimeEntryDTO newTimeEntry, string token);
        Task<TimeEntryDTO> AddNewTimeEntryAsync(NewTimeEntryDTO newTimeEntry, string token);

		TimeEntryDTO UpdatTimeEntry(long id, string token, NewTimeEntryDTO timeEntry);
		Task<TimeEntryDTO> UpdatTimeEntryAsync(long id, string token, NewTimeEntryDTO timeEntry);

		List<TimeEntryDTO> GetUserEntriesForDate(string date, string token);
		Task<List<TimeEntryDTO>> GetUserEntriesForDateAsync(string date, string token);
    }
}

