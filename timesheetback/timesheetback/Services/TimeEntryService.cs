using System;
using timesheetback.DTOs;
using timesheetback.Repositories;
using timesheetback.Models;
using NuGet.Common;
using Newtonsoft.Json.Linq;

namespace timesheetback.Services
{
	public class TimeEntryService : ITimeEntryService
	{

        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        private readonly double fullTimeWorkHours = 7.5;
        private readonly double hoursInDay = 24.0;

        public TimeEntryService(ITimeEntryRepository timeEntryRepository, IUserRepository userRepository, IJwtService jwtService)
		{
            _timeEntryRepository = timeEntryRepository;
            _userRepository = userRepository;
            _jwtService = jwtService;
		}

        public TimeEntryDTO AddNewTimeEntry(NewTimeEntryDTO newTimeEntry, string token)
        {
            string userEmail = _jwtService.GetClaimFromJWT(token, "email");
            var user = _userRepository.GetUserByEmail(userEmail) ?? throw new Exception("User with that email does not exists");
            if (DateTime.Parse(newTimeEntry.Date).CompareTo(DateTime.Now) == 1) {
                throw new Exception("Can't create TimeEntry for future.");
            }
            if (newTimeEntry.Hours <= 0 || newTimeEntry.Hours >= 24 || (newTimeEntry.Overtime != null && (newTimeEntry.Overtime < 0 || newTimeEntry.Overtime >= 24)))
            {
                throw new Exception("Invalid hours amount.");
            }
            List<TimeEntryDTO> userEnteriesForThisDay = GetUserEntriesForDate(newTimeEntry.Date, token);

            double totalHours = userEnteriesForThisDay.Sum(entry => entry.Hours);
            double totalOvertime = userEnteriesForThisDay.Sum(entry => entry.Overtime ?? 0);

            if (totalHours + newTimeEntry.Hours > fullTimeWorkHours)
            {
                throw new Exception($"Can't have more then {fullTimeWorkHours} reqular working hours. Everything after {fullTimeWorkHours} has to be overtime.");
            }

            if (totalHours + newTimeEntry.Hours < fullTimeWorkHours && totalOvertime + newTimeEntry.Overtime > 0) {
                throw new Exception("First enter regular hours then overtime");
            }

            double hoursWorkedThisDay = totalHours + totalOvertime;
            if(hoursWorkedThisDay + newTimeEntry.Hours + newTimeEntry.Overtime > hoursInDay) {
                throw new Exception($"Can't work more then {hoursInDay}h in one day.");
            }
            return new TimeEntryDTO(_timeEntryRepository.SaveEntry(user, new TimeEntry(newTimeEntry)));
        }

        public async Task<TimeEntryDTO> AddNewTimeEntryAsync(NewTimeEntryDTO newTimeEntry, string token)
        {
            string userEmail = _jwtService.GetClaimFromJWT(token, "email");
            var user = await _userRepository.GetUserByEmailAsync(userEmail) ?? throw new Exception("User with that email does not exists");
            if (DateTime.Parse(newTimeEntry.Date).CompareTo(DateTime.Now) == 1) {
                throw new Exception("Can't create TimeEntry for future");
            }
            if (newTimeEntry.Hours <= 0 || newTimeEntry.Hours >= 24 || (newTimeEntry.Overtime != null && (newTimeEntry.Overtime < 0 || newTimeEntry.Overtime >= 24)))
            {
                throw new Exception("Invalid hours amount.");
            }
            List<TimeEntryDTO> userEnteriesForThisDay = await GetUserEntriesForDateAsync(newTimeEntry.Date, token);

            double totalHours = userEnteriesForThisDay.Sum(entry => entry.Hours);
            double totalOvertime = userEnteriesForThisDay.Sum(entry => entry.Overtime ?? 0);

            if (totalHours + newTimeEntry.Hours > fullTimeWorkHours)
            {
                throw new Exception($"Can't have more then {fullTimeWorkHours} reqular working hours. Everything after {fullTimeWorkHours} has to be overtime.");
            }

            if (totalHours + newTimeEntry.Hours < fullTimeWorkHours && totalOvertime + newTimeEntry.Overtime > 0)
            {
                throw new Exception("First enter regular hours then overtime");
            }

            double hoursWorkedThisDay = totalHours + totalOvertime;
            if (hoursWorkedThisDay + newTimeEntry.Hours + newTimeEntry.Overtime > 24.0)
            {
                throw new Exception("Can't work more then 24h in one day.");
            }
            return new TimeEntryDTO(_timeEntryRepository.SaveEntry(user, new TimeEntry(newTimeEntry)));
        }

        public List<TotalTimeDTO> GetTotalTimesForDateRange(string start, string end, string token)
        {
            string userEmail = _jwtService.GetClaimFromJWT(token, "email");
            return _timeEntryRepository.GetTotalTimeEntriesForDateRange(userEmail, start, end);
        }

        public async Task<List<TotalTimeDTO>> GetTotalTimesForDateRangeAsync(string start, string end, string token)
        {
            string userEmail = _jwtService.GetClaimFromJWT(token, "email");
            return await _timeEntryRepository.GetTotalTimeEntriesForDateRangeAsync(userEmail, start, end);
        }

        public List<TimeEntryDTO> GetUserEntriesForDate(string date, string token)
        {
            string userEmail = _jwtService.GetClaimFromJWT(token, "email");
            List<TimeEntry> allEntries = _timeEntryRepository.GetAllUserEntriesForDate(userEmail, date);
            return allEntries.Select(entry => new TimeEntryDTO(entry)).ToList();
        }

        public async Task<List<TimeEntryDTO>> GetUserEntriesForDateAsync(string date, string token)
        {
            string userEmail = _jwtService.GetClaimFromJWT(token, "email");
            List<TimeEntry> allEntries = await _timeEntryRepository.GetAllUserEntriesForDateAsync(userEmail, date);
            return allEntries.Select(entry => new TimeEntryDTO(entry)).ToList();
        }

        public TimeEntryDTO UpdatTimeEntry(long id, string token, NewTimeEntryDTO timeEntry)
        { 
            TimeEntry timeEntryToUpdate = _timeEntryRepository.GetEntryById(id) ?? throw new Exception("TimeEntry with that id does not exist");
            if (DateTime.Parse(timeEntry.Date).CompareTo(DateTime.Now) == 1)
            {
                throw new Exception("Can't create TimeEntry for future.");
            }
            if (timeEntry.Hours <= 0 || timeEntry.Hours >= 24 || (timeEntry.Overtime != null && (timeEntry.Overtime < 0 || timeEntry.Overtime >= 24)))
            {
                throw new Exception("Invalid hours amount.");
            }
            List<TimeEntryDTO> userEnteriesForThisDay = GetUserEntriesForDate(timeEntry.Date, token);

            double totalHours = userEnteriesForThisDay.Sum(entry => entry.Hours);
            double totalOvertime = userEnteriesForThisDay.Sum(entry => entry.Overtime ?? 0);

            double hoursWorkedThisDay = totalHours + totalOvertime;
            if (hoursWorkedThisDay + timeEntry.Hours + timeEntry.Overtime > 24.0)
            {
                throw new Exception("Can't work more then 24h in one day.");
            }
            return new TimeEntryDTO(_timeEntryRepository.UpdateEntry(timeEntryToUpdate, timeEntry));
        }

        public async Task<TimeEntryDTO> UpdatTimeEntryAsync(long id, string token, NewTimeEntryDTO timeEntry)
        {
            TimeEntry timeEntryToUpdate = await _timeEntryRepository.GetEntryByIdAsync(id) ?? throw new Exception("TimeEntry with that id does not exist");
            if (DateTime.Parse(timeEntry.Date).CompareTo(DateTime.Now) == 1)
            {
                throw new Exception("Can't create TimeEntry for future.");
            }
            if (timeEntry.Hours <= 0 || timeEntry.Hours >= 24 || (timeEntry.Overtime != null && (timeEntry.Overtime < 0 || timeEntry.Overtime >= 24)))
            {
                throw new Exception("Invalid hours amount.");
            }
            List<TimeEntryDTO> userEnteriesForThisDay = GetUserEntriesForDate(timeEntry.Date, token);

            double totalHours = userEnteriesForThisDay.Sum(entry => entry.Hours);
            double totalOvertime = userEnteriesForThisDay.Sum(entry => entry.Overtime ?? 0);

            double hoursWorkedThisDay = totalHours + totalOvertime;
            if (hoursWorkedThisDay + timeEntry.Hours + timeEntry.Overtime > 24.0)
            {
                throw new Exception("Can't work more then 24h in one day.");
            }
            return new TimeEntryDTO(_timeEntryRepository.UpdateEntry(timeEntryToUpdate, timeEntry));
        }
    }
}

