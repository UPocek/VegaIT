using System;
namespace timesheetback.Services
{
	public interface IHelperService
	{
		Task SendForgotPasswordEmail(string emailSubject, string emailTo, string nameTo, string code);

		int GenerateRandom4DigitNumber();

		string GenerateRandomStringCode();

    }
}

