using System;
namespace timesheetback.DTOs
{
	public class ForgotPasswordDTO
	{
		public string Email { get; set; }

		public ForgotPasswordDTO()
		{
		}

        public ForgotPasswordDTO(string email)
        {
            Email = email;
        }
    }
}

