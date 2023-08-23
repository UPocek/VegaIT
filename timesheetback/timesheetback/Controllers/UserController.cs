using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using timesheetback.DTOs;
using timesheetback.Models;
using timesheetback.Services;

namespace timesheetback.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    // /api/user/login
    [HttpPost("login")]
    public async Task<ActionResult<UserCredentialsDTO>> Login(LoginCredentialsDTO loginCredentials)
    {
        try {
           return await _userService.ProccessUserLoginAsync(loginCredentials);
        }
        catch {
            return NotFound();
        }
        
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration(RegistrationCredentialsDTO registrationCredentials)
    {
        try
        {
            await _userService.ProccessUserRegistrationAsync(registrationCredentials);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }

    }

    [HttpGet("roles")]
    public async Task<List<RoleDTO>> GetAllRoles() {
        return await _userService.GetAllRolesAsync();
    }
}

