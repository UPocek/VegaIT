using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using timesheetback.Models;

namespace timesheetback.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeEntryController : ControllerBase
	{
        public TimeEntryController()
        {
        }
    }
}

