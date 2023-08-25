using System;
using System.Net;
using timesheetback.Models;

namespace timesheetback.DTOs
{
	public class ClientDTO
	{
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long CountryId { get; set; }
        public long CityId { get; set; }

        public ClientDTO()
		{
		}

        public ClientDTO(long id, string name, string address, long countryId, long cityId)
        {
            Id = id;
            Name = name;
            Address = address;
            CountryId = countryId;
            CityId = cityId;
        }

        public ClientDTO(Client client)
        {
            Id = client.Id;
            Name = client.Name;
            Address = client.Address;
            CountryId = client.CountryId;
            CityId = client.CountryId;
        }
    }
}

