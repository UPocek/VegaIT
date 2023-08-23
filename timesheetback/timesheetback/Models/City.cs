using System;
namespace timesheetback.Models
{
	public class City
	{

		public long Id { get; set; }
		public string Name { get; set; }
		public string Zip { get; set; }

		public City()
		{
		}

        public City(long id, string name, string zip)
        {
            Id = id;
            Name = name;
            Zip = zip;
        }
    }
}

