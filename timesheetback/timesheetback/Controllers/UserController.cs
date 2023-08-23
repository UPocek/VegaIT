using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using timesheetback.Models;

namespace timesheetback.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly TimeSheetContext _context;

    public UserController(ILogger<UserController> logger, TimeSheetContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<List<Category>> GetAllCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories;
    }
}

