using System;
namespace timesheetback.DTOs
{
	public class UserCredentialsDTO
	{

        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public UserCredentialsDTO()
		{
		}
	}
}

