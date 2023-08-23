using System;
namespace timesheetback.DTOs
{
	public class RegistrationCredentialsDTO
	{

		public string Name { get; set; }
		public string Username { get; set; }
		public string Role { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public RegistrationCredentialsDTO()
		{
		}

        public RegistrationCredentialsDTO(string name, string username, string role, string email, string password)
        {
            Name = name;
            Username = username;
            Role = role;
            Email = email;
            Password = password;
        }
    }
}

