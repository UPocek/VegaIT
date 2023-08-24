using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class CreateClientCredentialsDTO
	{

        public string Name { get; set; }
        public string Address { get; set; }
        public long CountryId { get; set; }
        public long CityId { get; set; }

        public CreateClientCredentialsDTO()
		{
		}

        public CreateClientCredentialsDTO(string name, string address, long countryId, long cityId)
        {
            Name = name;
            Address = address;
            CountryId = countryId;
            CityId = cityId;
        }
    }
}

