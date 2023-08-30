using System;
namespace timesheetback.Services
{
	public interface IJwtService
	{
        string GetClaimFromJWT(string jwtToken, string claimName);

    }
}

