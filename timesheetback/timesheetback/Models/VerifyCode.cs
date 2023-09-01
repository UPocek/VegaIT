using System;
namespace timesheetback.Models
{
	public class VerifyCode
	{
		public long Id { get; set; }
		public string Code { get; set; }
		public string Email { get; set; }
		public bool Verified { get; set; }

		public VerifyCode()
		{
		}

        public VerifyCode(string code, string email, bool verified)
        {
            Code = code;
            Email = email;
            Verified = verified;
        }
    }
}

