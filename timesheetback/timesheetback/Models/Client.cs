using System;
namespace timesheetback.Models
{
	public class Client
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public long CountryId { get; set; }
		public Country Country { get; set; } = null!;
		public long CityId { get; set; }
		public City City { get; set; } = null!;


        public Client()
		{
		}

        public Client(long id, string name, string address, long countryId, Country country, long cityId, City city)
        {
            Id = id;
            Name = name;
            Address = address;
            CountryId = countryId;
            Country = country;
            CityId = cityId;
            City = city;
        }
    }
}

