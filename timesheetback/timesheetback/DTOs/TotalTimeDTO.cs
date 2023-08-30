using System;
namespace timesheetback.DTOs
{
	public class TotalTimeDTO
	{
		public DateTime Date { get; set; }
		public double Time { get; set; }

		public TotalTimeDTO()
		{
		}

        public TotalTimeDTO(DateTime date, double time)
        {
            Date = date;
            Time = time;
        }
    }
}

