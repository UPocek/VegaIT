using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
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
    public async Task<ActionResult<UserDTO>> Registration(RegistrationCredentialsDTO registrationCredentials)
    {
        try
        {
            return await _userService.ProccessUserRegistrationAsync(registrationCredentials);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpGet("all")]
    public async Task<List<UserDTO>> GetAllUsers()
    {
        return await _userService.GetAllUsersAsync();
    }

    [HttpGet("all-minimal")]
    public async Task<List<UserMinimalDTO>> GetAllUsersMinimalInfo()
    {
        return await _userService.GetAllUsersMinimalAsync();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDTO>> UpdateUser(long id, RegistrationCredentialsDTO registrationCredentials)
    {
        try
        {
            return await _userService.UpdateUserAsync(id, registrationCredentials);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [AllowAnonymous]
    [HttpPut("forgotPassword")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO email)
    {
        await _userService.SendRecoveryEmailAsync(email);
        return Ok("Reset password email sent");
    }

    [AllowAnonymous]
    [HttpPut("newpassword")]
    public async Task<IActionResult> AssignNewPassword(NewPasswordDTO credentials)
    {
        try
        {
            await _userService.AssignNewPasswordAsync(credentials);
            return Ok("New password set.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("changepassword")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ChangePassword(ChangePasswordDTO newPassword)
    {
        try
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            await _userService.ChangePasswordAsync(newPassword, token!);
            return Ok("Password changed.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}

