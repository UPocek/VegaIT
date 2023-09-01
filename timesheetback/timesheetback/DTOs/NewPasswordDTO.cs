using System;
namespace timesheetback.DTOs
{
	public class NewPasswordDTO
	{
		public string Code { get; set; }
		public string Password { get; set; }

		public NewPasswordDTO()
		{
		}

        public NewPasswordDTO(string code, string password)
        {
            Code = code;
            Password = password;
        }
    }
}

