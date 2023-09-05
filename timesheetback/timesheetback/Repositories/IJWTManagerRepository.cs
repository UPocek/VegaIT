using System;
using timesheetback.DTOs;

namespace timesheetback.Repositories
{
	public interface IJWTManagerRepository
	{
        UserCredentialsDTO Authenticate(LoginCredentialsDTO loginCredentials);
    }
}

