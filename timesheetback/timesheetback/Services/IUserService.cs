using System;
using timesheetback.DTOs;
using timesheetback.Models;

namespace timesheetback.Services
{
	public interface IUserService
	{
        Employee? ProccessUserLogin(LoginCredentialsDTO loginCredentials);
        Task<Employee?> ProccessUserLoginAsync(LoginCredentialsDTO loginCredentials);

        Task ProccessUserRegistrationAsync(RegistrationCredentialsDTO registrationCredentials);
        void ProccessUserRegistration(RegistrationCredentialsDTO registrationCredentials);

        List<RoleDTO> GetAllRoles();
        Task<List<RoleDTO>> GetAllRolesAsync();

        List<UserDTO> GetAllUsers();
        Task<List<UserDTO>> GetAllUsersAsync();

        List<UserMinimalDTO> GetAllUsersMinimal();
        Task<List<UserMinimalDTO>> GetAllUsersMinimalAsync();
    }
}

