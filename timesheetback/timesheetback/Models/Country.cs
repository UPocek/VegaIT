using System;
namespace timesheetback.Models
{
	public class Country
	{

		public long Id { get; set; }
		public string Name { get; set; }

		public Country()
		{
		}

        public Country(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

