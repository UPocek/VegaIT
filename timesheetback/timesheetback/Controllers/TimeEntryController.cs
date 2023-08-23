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
        private readonly TimeSheetContext _context;

        public TimeEntryController(TimeSheetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
    }
}

