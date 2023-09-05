using System;
namespace timesheetback.DTOs
{
	public class ChangePasswordDTO
	{
		public string Password { get; set; }

		public ChangePasswordDTO()
		{
		}

        public ChangePasswordDTO(string password)
        {
            Password = password;
        }
    }
}

