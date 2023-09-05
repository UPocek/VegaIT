using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class CountryDTO
	{
        public long Id { get; set; }
        public string Name { get; set; }

        public CountryDTO()
		{
		}

        public CountryDTO(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public CountryDTO(Country country) {
            Id = country.Id;
            Name = country.Name;
        }
    }
}

