using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using timesheetback.DTOs;
using timesheetback.Models;
using timesheetback.Repositories;
using timesheetback.Services;

namespace timesheetback.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IJWTManagerRepository _jWTManager;

    public UserController(ILogger<UserController> logger, IUserService userService, IJWTManagerRepository jWTManager)
    {
        _logger = logger;
        _userService = userService;
        _jWTManager = jWTManager;
    }

    // /api/user/login
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginCredentialsDTO loginCredentials)
    {
        var token = _jWTManager.Authenticate(loginCredentials);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    [AllowAnonymous]
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

