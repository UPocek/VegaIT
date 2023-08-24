using System;
using timesheetback.DTOs;

namespace timesheetback.Services
{
	public interface IUserService
	{

        UserCredentialsDTO ProccessUserLogin(LoginCredentialsDTO loginCredentials);

        Task<UserCredentialsDTO> ProccessUserLoginAsync(LoginCredentialsDTO loginCredentials);

        Task ProccessUserRegistrationAsync(RegistrationCredentialsDTO registrationCredentials);

        void ProccessUserRegistration(RegistrationCredentialsDTO registrationCredentials);

        List<RoleDTO> GetAllRoles();

        Task<List<RoleDTO>> GetAllRolesAsync();
    }
}

