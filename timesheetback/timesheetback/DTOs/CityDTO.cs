using System;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class CityDTO
	{
        public long Id { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }

        public CityDTO()
		{
		}

        public CityDTO(long id, string name, string zip)
        {
            Id = id;
            Name = name;
            Zip = zip;
        }

        public CityDTO(City city) {
            Id = city.Id;
            Name = city.Name;
            Zip = city.Zip;
        }
    }
}

