﻿using System;
using timesheetback.DTOs;
using timesheetback.Models;

namespace timesheetback.Services
{
	public interface IUserService
	{
        Employee? ProccessUserLogin(LoginCredentialsDTO loginCredentials);
        Task<Employee?> ProccessUserLoginAsync(LoginCredentialsDTO loginCredentials);

        UserDTO ProccessUserRegistration(RegistrationCredentialsDTO registrationCredentials);
        Task<UserDTO> ProccessUserRegistrationAsync(RegistrationCredentialsDTO registrationCredentials);

        List<UserDTO> GetAllUsers();
        Task<List<UserDTO>> GetAllUsersAsync();

        List<UserMinimalDTO> GetAllUsersMinimal();
        Task<List<UserMinimalDTO>> GetAllUsersMinimalAsync();

        UserDTO UpdateUser(long id, RegistrationCredentialsDTO registrationCredentials);
        Task<UserDTO> UpdateUserAsync(long id, RegistrationCredentialsDTO registrationCredentials);

        void DeleteUser(long id);
        Task DeleteUserAsync(long id);
    }
}

