using System;
using Microsoft.EntityFrameworkCore;
using timesheetback.Models;
using timesheetback.DTOs;

namespace timesheetback.Repositories
{
	public class TimeEntryRepository : ITimeEntryRepository
	{

        private readonly TimeSheetContext _context;

		public TimeEntryRepository(TimeSheetContext context)
		{
            _context = context;
		}

        public List<TimeEntry> GetAllUserEntriesForDate(string userEmail, string dateIso)
        {
            if (!DateTime.TryParse(dateIso, out DateTime date))
            {
                throw new ArgumentException("Invalid date format.");
            }

            return _context.TimeEntries
            .Where(entity => entity.Date.Date == date.Date && entity.Employee != null && entity.Employee.Email == userEmail)
            .ToList();

        }

        public Task<List<TimeEntry>> GetAllUserEntriesForDateAsync(string userEmail, string dateIso)
        {
            if (!DateTime.TryParse(dateIso, out DateTime date))
            {
                throw new ArgumentException("Invalid date format.");
            }

            return _context.TimeEntries
            .Where(entity => entity.Date.Date == date.Date && entity.Employee != null && entity.Employee.Email == userEmail)
            .ToListAsync();
        }

        public TimeEntry? GetEntryById(long id)
        {
            return _context.TimeEntries.Find(id);
        }

        public Task<TimeEntry?> GetEntryByIdAsync(long id)
        {
            return _context.TimeEntries.FirstOrDefaultAsync(entry => entry.Id == id);
        }

        public List<TimeEntryDetailedDTO> GetTimeEntriesReport(long client, long project, long category, long employee, string time)
        {
            var query = _context.TimeEntries.AsQueryable();
            if (client != -1)
            {
                query = query.Where(te => te.ClientId == client);
            }

            if (project != -1)
            {
                query = query.Where(te => te.ProjectId == project);
            }

            if (category != -1)
            {
                query = query.Where(te => te.CategoryId == category);
            }

            if (employee != -1)
            {
                query = query.Where(te => te.EmployeeId == employee);
            }

            if(time != "all") {

                var today = DateTime.Today;

                if (time == "this_week") {
                    // Assuming Monday is the start of the week
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                    var endOfWeek = startOfWeek.AddDays(6);
                    query = query.Where(te => te.Date >= startOfWeek && te.Date <= endOfWeek);
                }
                else if (time == "last_week")
                {
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek -7);
                    var endOfWeek = startOfWeek.AddDays(6);
                    query = query.Where(te => te.Date >= startOfWeek && te.Date <= endOfWeek);
                }
                else if (time == "this_month")
                {
                    var startOfMonth = today.AddDays(-today.Day);
                    var endOfWeek = startOfMonth.AddMonths(1);
                    query = query.Where(te => te.Date >= startOfMonth && te.Date <= endOfWeek);
                }
                else if (time == "last_month")
                {
                    var startOfMonth = today.AddMonths(-1).AddDays(-today.Day);
                    var endOfWeek = startOfMonth.AddMonths(1);
                    query = query.Where(te => te.Date >= startOfMonth && te.Date <= endOfWeek);
                }
                else if (time.Contains('|')) {
                    var borders = time.Split('|');
                    if (borders[0] != "") {
                        var start = DateTime.Parse(borders[0]);
                        query = query.Where(te => te.Date >= start);
                    }
                    if (borders[1] != "")
                    {
                        var end = DateTime.Parse(borders[1]);
                        query = query.Where(te => te.Date <= end);
                    }
                }
            }

            var timeEntries = query.Select(te => new TimeEntryDetailedDTO
            {
                Id = te.Id,
                Description = te.Description,
                Hours = te.Hours,
                Overtime = te.Overtime,
                Date = te.Date,
                EmployeeName = (te.Employee != null ? te.Employee.Name : ""),
                ClientName = (te.Client != null ? te.Client.Name : ""),
                ProjectName = (te.Project != null ? te.Project.Name : ""),
                CategoryName = (te.Category != null ? te.Category.Name : ""),
            }).ToList();

            return timeEntries;
        }

        public Task<List<TimeEntryDetailedDTO>> GetTimeEntriesReportAsync(long client, long project, long category, long employee, string time)
        {
            var query = _context.TimeEntries.AsQueryable();
            if (client != -1)
            {
                query = query.Where(te => te.ClientId == client);
            }

            if (project != -1)
            {
                query = query.Where(te => te.ProjectId == project);
            }

            if (category != -1)
            {
                query = query.Where(te => te.CategoryId == category);
            }

            if (employee != -1)
            {
                query = query.Where(te => te.EmployeeId == employee);
            }

            if (time != "all")
            {

                var today = DateTime.Today;

                if (time == "this_week")
                {
                    // Assuming Monday is the start of the week
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                    var endOfWeek = startOfWeek.AddDays(6);
                    query = query.Where(te => te.Date >= startOfWeek && te.Date <= endOfWeek);
                }
                else if (time == "last_week")
                {
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek - 7);
                    var endOfWeek = startOfWeek.AddDays(6);
                    query = query.Where(te => te.Date >= startOfWeek && te.Date <= endOfWeek);
                }
                else if (time == "this_month")
                {
                    var startOfMonth = today.AddDays(-today.Day);
                    var endOfWeek = startOfMonth.AddMonths(1);
                    query = query.Where(te => te.Date >= startOfMonth && te.Date <= endOfWeek);
                }
                else if (time == "last_month")
                {
                    var startOfMonth = today.AddMonths(-1).AddDays(-today.Day);
                    var endOfWeek = startOfMonth.AddMonths(1);
                    query = query.Where(te => te.Date >= startOfMonth && te.Date <= endOfWeek);
                }
                else if (time.Contains('|'))
                {
                    var borders = time.Split('|');
                    if (borders[0] != "")
                    {
                        var start = DateTime.Parse(borders[0]);
                        query = query.Where(te => te.Date >= start);
                    }
                    if (borders[1] != "")
                    {
                        var end = DateTime.Parse(borders[1]);
                        query = query.Where(te => te.Date <= end);
                    }
                }
            }

            var timeEntries = query.Select(te => new TimeEntryDetailedDTO
            {
                Id = te.Id,
                Description = te.Description,
                Hours = te.Hours,
                Overtime = te.Overtime,
                Date = te.Date,
                EmployeeName = (te.Employee != null ? te.Employee.Name : ""),
                ClientName = (te.Client != null ? te.Client.Name : ""),
                ProjectName = (te.Project != null ? te.Project.Name : ""),
                CategoryName = (te.Category != null ? te.Category.Name : ""),
            }).ToListAsync();

            return timeEntries;
        }

        public List<TotalTimeDTO> GetTotalTimeEntriesForDateRange(string userEmail, string startIsoDate, string endIsoDate)
        {
            if (!DateTime.TryParse(startIsoDate, out DateTime startDate) ||
             !DateTime.TryParse(endIsoDate, out DateTime endDate))
            {
                throw new ArgumentException("Invalid date format.");
            }

            var dateRange = Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
            .Select(offset => startDate.AddDays(offset).Date)
            .ToList();

            var aggregatedSums = _context.TimeEntries
            .Where(entity => entity.Date >= startDate && entity.Date <= endDate && entity.Employee != null && entity.Employee.Email == userEmail)
            .GroupBy(entity => entity.Date.Date)
            .Select(group => new TotalTimeDTO
            {
                Date = group.Key,
                Time = group.Sum(entity => entity.Hours)
            })
            .ToList();

            var aggregatedSumsWithZeros = new List<TotalTimeDTO>();

            foreach (var dateToMatch in dateRange)
            {
                double time = 0;
                foreach (var entry in aggregatedSums)
                {
                    if (entry.Date == dateToMatch)
                    {
                        time = entry.Time;
                    }
                }
                aggregatedSumsWithZeros.Add(new TotalTimeDTO(dateToMatch, time));
            }

            return aggregatedSumsWithZeros;
        }

        public async Task<List<TotalTimeDTO>> GetTotalTimeEntriesForDateRangeAsync(string userEmail, string startIsoDate, string endIsoDate)
        {
            if (!DateTime.TryParse(startIsoDate, out DateTime startDate) ||
              !DateTime.TryParse(endIsoDate, out DateTime endDate))
            {
                throw new ArgumentException("Invalid date format.");
            }

            var dateRange = Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
            .Select(offset => startDate.AddDays(offset).Date)
            .ToList();

            var aggregatedSums = await _context.TimeEntries
            .Where(entity => entity.Date >= startDate && entity.Date <= endDate && entity.Employee != null && entity.Employee.Email == userEmail)
            .GroupBy(entity => entity.Date.Date)
            .Select(group => new TotalTimeDTO
            {
                Date = group.Key,
                Time = group.Sum(entity => entity.Hours) 
            })
            .ToListAsync();

            var aggregatedSumsDictionary = aggregatedSums.ToDictionary(sum => sum.Date);

            var aggregatedSumsWithZeros = dateRange.Select(dateToMatch =>
            {
                if (aggregatedSumsDictionary.TryGetValue(dateToMatch, out var time))
                {
                    return new TotalTimeDTO(dateToMatch, time.Time);
                }
                else
                {
                    return new TotalTimeDTO(dateToMatch, 0);
                }
            }).ToList();

            return aggregatedSumsWithZeros;
        }

        public TimeEntry SaveEntry(Employee user, TimeEntry newTimeEntry)
        {
            user.TimeEntrys.Add(newTimeEntry);
            newTimeEntry.EmployeeId = user.Id;
            _context.TimeEntries.Add(newTimeEntry);
            _context.SaveChanges();
            return newTimeEntry;
        }

        public TimeEntry UpdateEntry(TimeEntry timeEntryToUpdate, NewTimeEntryDTO newTimeEntry)
        {
            timeEntryToUpdate.Description = newTimeEntry.Description;
            timeEntryToUpdate.Hours = newTimeEntry.Hours;
            timeEntryToUpdate.Overtime = newTimeEntry.Overtime;
            timeEntryToUpdate.ClientId = newTimeEntry.ClientId;
            timeEntryToUpdate.ProjectId = newTimeEntry.ProjectId;
            timeEntryToUpdate.CategoryId = newTimeEntry.CategoryId;

            _context.SaveChanges();

            return timeEntryToUpdate;
        }
    }
}

