using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using timesheetback.DTOs;
using timesheetback.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace timesheetback.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeEntryController : ControllerBase
	{
        private readonly ITimeEntryService _timeEntryService;
        public TimeEntryController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        [HttpGet("range")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<TotalTimeDTO>>> GetTotalTimes(string start, string end)
        {
            try
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                return await _timeEntryService.GetTotalTimesForDateRangeAsync(start, end, token!);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("fordate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<TimeEntryDTO>>> GetAllUserEnteries(string date)
        {
            try
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                return await _timeEntryService.GetUserEntriesForDateAsync(date, token!);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<TimeEntryDTO>> AddEntry(NewTimeEntryDTO newTimeEntry)
        {
            try
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                return await _timeEntryService.AddNewTimeEntryAsync(newTimeEntry, token!);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<TimeEntryDTO>> UpdateEntry(long id, NewTimeEntryDTO timeEntry)
        {
            try
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                return await _timeEntryService.UpdatTimeEntryAsync(id, token!, timeEntry!);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

